import { Component, OnInit } from '@angular/core';
import { LeadsService } from '../../services/common.service';
import { CookieService } from 'ngx-cookie-service';
import {
  Lead,
  ProductList,
  ViewFamilyData,
  ViewFamilyMembersFilter,
  ViewLeadsFilter,
  ViewLeadSubmitterData
} from '../../Datamodels/datarequest';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageService } from 'primeng/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-houses',
  templateUrl: './houses.component.html',
  styleUrl: './houses.component.css',
})
export class HousesComponent implements OnInit {
  ViewLeads: ViewLeadsFilter[] = [];
  ViewFamilyMembers: ViewFamilyMembersFilter[] = [];
  LeadsList: Lead[] = [];
  PrductList: ProductList[] = [];
  MembersList: ViewFamilyData = new ViewFamilyData();
  ViewLeadsData: ViewFamilyData = new ViewFamilyData();
  ViewLeadsSubmitterData: ViewLeadSubmitterData = new ViewLeadSubmitterData();
  UserId: string = '';
  UserRole: string = '';
  LPID: string = '';
  HouseID: number = 0;
  LeadRefNo: string = '';

  metaKey: boolean = true;
  visible: boolean = false;

  HouseForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private leadsService: LeadsService,
    private cookieService: CookieService,
    private spinner: NgxSpinnerService,
    private messageService: MessageService,
  ) {
    this.HouseForm = this.fb.group({
      FamilyNo: ['', Validators.required],
      HouseUnitNo: ['', Validators.required],
      SubNo: ['', Validators.required],
      HouseholdNo: ['', Validators.required],
    });
  }

  get familyNoControl() {
    return this.HouseForm.get('FamilyNo');
  }
  get houseUnitNoControl() {
    return this.HouseForm.get('HouseUnitNo');
  }
  get subNoControl() {
    return this.HouseForm.get('SubNo');
  }
  get householdNoControl() {
    return this.HouseForm.get('HouseholdNo');
  }

  get isStepValid(): boolean {
    return this.HouseForm.valid;
  }

  async ngOnInit() {
    this.spinner.show();
    await this.GetFamilyMembers();
    this.spinner.hide();
  }

  async GetFamilyMembers() {
    this.UserId = this.cookieService.get('e_userid');
    this.UserRole = this.cookieService.get('e_role');
    const reqParams = { UserId: this.UserId, UserRole: this.UserRole };

    this.leadsService.GetFamilyMembers(reqParams).subscribe((resp) => {
      if (resp) {
        this.ViewFamilyMembers = resp;
      } else {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Data Load Failed',
        });
      }
    });
  }

  onRowSelect(event: any) {
    this.visible = true;
    this.spinner.show();
    this.HouseID = event.data.id;
    this.ViewLeadsData = event.data;

    this.HouseForm.patchValue({
      FamilyNo: this.ViewLeadsData.familyNo,
      HouseUnitNo: this.ViewLeadsData.houseUnitNo,
      SubNo: this.ViewLeadsData.subNo,
      HouseholdNo: this.ViewLeadsData.householdNo,
    });

    this.leadsService.GetHousebyID(this.HouseID).subscribe((resp) => {
      if (resp) {
        this.MembersList = resp;
        console.log('Members List:', this.MembersList);
        this.spinner.hide();
      } else {
        this.spinner.hide();
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Data Load Failed',
        });
      }
    });
  }

  UpdateHouse() {
    if (this.HouseForm.valid) {
      const houseData = {
        ID: this.HouseID,
        FamilyNo: this.HouseForm.value.FamilyNo,
        HouseUnitNo: this.HouseForm.value.HouseUnitNo,
        SubNo: this.HouseForm.value.SubNo,
        HouseholdNo: this.HouseForm.value.HouseholdNo,
      };

      this.leadsService
        .UpdateHouse(houseData)
        .then(async (resp) => {
          if (resp && resp !== 0) {
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'House Updated Successfully',
            });
            this.visible = false;
            await this.GetFamilyMembers();
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'Failed to Update House',
            });
          }
        })
        .catch(() => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Something went wrong during update',
          });
        });
    } else {
      this.messageService.add({
        severity: 'warn',
        summary: 'Warning',
        detail: 'Please fill all required fields',
      });
    }
  }

  DeleteHouse() {
    const houseData = { ID: this.HouseID };
    this.leadsService.DeleteHouse(houseData).then(async (resp) => {
      if (resp) {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'House Deleted Successfully',
        });
        this.HouseForm.reset();
        this.visible = false;
        await this.GetFamilyMembers();
      } else {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to Delete House',
        });
      }
    });
  }

  DeleteMember(member: any) {
    const memberData = { ID: member.id };

    this.leadsService.DeleteMember(memberData).then(async (resp) => {
      if (resp) {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Member Deleted Successfully',
        });
        this.HouseForm.reset();
        this.visible = false;
        await this.GetFamilyMembers();
      } else {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to Delete Member',
        });
      }
    });
  }

  getSeverity(
    status: string,
  ): 'success' | 'secondary' | 'info' | 'warning' | 'danger' | 'contrast' {
    switch (status) {
      case 'Accept':
        return 'success';
      case 'Reject':
        return 'danger';
      case 'In Progress':
        return 'info';
      default:
        return 'danger';
    }
  }
}
