import { Component } from '@angular/core';
import { LeadsService } from '../../../services/common.service';
import { CookieService } from 'ngx-cookie-service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageService } from 'primeng/api';
import { ViewOfficersFilter } from '../../../Datamodels/datarequest';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-officer-config',
  templateUrl: './officer-config.component.html',
  styleUrl: './officer-config.component.css'
})
export class OfficerConfigComponent {
  UserId: string = '';
  UserRole: string = '';
  OfficerName: string = '';
  ViewOfficers: ViewOfficersFilter[] = [];
  ViewLeadsData: ViewOfficersFilter = new ViewOfficersFilter();
  metaKey: boolean = true;
  visible: boolean = false;

  StatusList: { label: string; value: boolean }[] = [
    { label: 'Active', value: true },
    { label: 'Deactive', value: false }
  ];

  OfficersForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private leadsService: LeadsService,
    private cookieService: CookieService,
    private spinner: NgxSpinnerService,
    private messageService: MessageService
  ) {

    this.OfficersForm = this.fb.group({
      Firstname: ['', Validators.required],
      UserEmail: ['', Validators.required],
      Mobile: ['', Validators.required],
      WasamName: ['', Validators.required],
      Lastname: ['', Validators.required],
      selectedStatus: ['', Validators.required],
    });
  }

  get firstnameControl() {
    return this.OfficersForm.get('Firstname');
  }
  get userEmailControl() {
    return this.OfficersForm.get('UserEmail');
  }
  get mobileControl() {
    return this.OfficersForm.get('Mobile');
  }
  get wasamNameControl() {
    return this.OfficersForm.get('WasamName');
  }
  get lastnameControl() {
    return this.OfficersForm.get('Lastname');
  }
  get selectedStatusControl() {
    return this.OfficersForm.get('selectedStatus');
  }

  get isStepValid(): boolean {
    return this.OfficersForm.valid;
  }

  async ngOnInit() {
    this.spinner.show();
    await this.GetAllOfficers();
    this.spinner.hide();
  }

  async GetAllOfficers() {
    this.UserId = this.cookieService.get('e_userid');
    this.UserRole = this.cookieService.get('e_role');
    const reqParams = { UserId: this.UserId, UserRole: this.UserRole };

    this.leadsService.GetAllOfficers(reqParams).subscribe(resp => {
      if (resp) {
        this.ViewOfficers = resp;
        this.ViewLeadsData = resp;
      } else {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Data Load Failed' });
      }
    });
  }

  onRowSelect(event: any) {
    this.visible = true;
    this.ViewLeadsData = event.data;
    
    this.OfficersForm.patchValue({
      Firstname: this.ViewLeadsData.firstname,
      UserEmail: this.ViewLeadsData.userEmail,
      Mobile: this.ViewLeadsData.mobile,
      WasamName: this.ViewLeadsData.wasamName,
      Lastname: this.ViewLeadsData.lastname,
      selectedStatus: this.ViewLeadsData.status
    });
    this.OfficersForm.get('WasamName')?.disable();
  }

  Save() {
    if (this.OfficersForm.valid) {
      this.spinner.show();
      const formData = this.OfficersForm.value;
      const reqParams = {
        ID: this.ViewLeadsData.id,
        Firstname: formData.Firstname,
        Email: formData.UserEmail,
        Mobile: formData.Mobile,
        Lastname: formData.Lastname,
        IsActive: formData.selectedStatus
      };

      this.leadsService.UpdateAppUsers(reqParams).then(async resp => {
        if (resp) {
          await this.GetAllOfficers();
          this.spinner.hide();
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Update successfully' });
          this.OfficersForm.reset();
          this.visible = false;
        } else {
          this.spinner.hide();
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to save officer' });
          this.OfficersForm.reset();
          this.visible = false;
        }
      }).catch(() => {
        this.spinner.hide();
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to save officer' });
        this.OfficersForm.reset();
        this.visible = false;
      });
    } else {
      this.spinner.hide();
      this.messageService.add({ severity: 'warn', summary: 'Warning', detail: 'Please fill all required fields' });
      this.OfficersForm.reset();
      this.visible = false;
    }
  }

  Delete() {
    this.spinner.show();
    const officerId = this.ViewLeadsData.id;

    this.leadsService.DeleteAppUser(officerId).then(async resp => {
      if (resp) {
        await this.GetAllOfficers();
        this.spinner.hide();
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Officer Remove successfully' });
        this.OfficersForm.reset();
        this.visible = false;
      } else {
        this.spinner.hide();
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to delete officer' });
        this.OfficersForm.reset();
        this.visible = false;
      }
    }).catch(() => {
      this.spinner.hide();
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to delete officer' });
      this.OfficersForm.reset();
      this.visible = false;
    });
  }

  getSeverity(status: boolean): "success" | "danger" {
    return status ? 'success' : 'danger';
  }
}
