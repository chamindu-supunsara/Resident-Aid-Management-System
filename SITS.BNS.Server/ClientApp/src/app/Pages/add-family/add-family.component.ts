import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { LeadForm } from '../../Datamodels/daraforms';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MessageService } from 'primeng/api';
import { LeadsService } from '../../services/common.service';
import { Stepper } from 'primeng/stepper';
import { NgxSpinnerService } from 'ngx-spinner';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import {
  AidSelections,
  FamilyMembers,
  leadData,
} from '../../Datamodels/datarequest';

interface Product {
  id: number;
  name: string;
  code: string;
  amount?: number | null;
  remark?: string;
  measurement: string;
}

interface Company {
  name: string;
  code: string;
  products: Product[];
}

@Component({
  selector: 'app-add-family',
  templateUrl: './add-family.component.html',
  styleUrl: './add-family.component.css',
})
export class AddFamilyComponent implements OnInit {
  @ViewChild('stepper') stepper: Stepper | undefined;

  Companies: Company[] = [];
  selectedCompanies: Company[] = [];
  FamilyMembers: FamilyMembers[] = [];
  FamilyChilds: FamilyMembers[] = [];
  leadData: leadData = new leadData();

  UserId: string = '';
  OtherAid: string = '';
  LeadForm: leadData = new leadData();
  LeadGenerateForm: FormGroup;

  selectedProducts: { [key: string]: Product[] } = {};
  productDetails: { [key: string]: Product[] } = {};

  GenderList: { label: string; value: string }[] = [
    { label: 'Male', value: 'male' },
    { label: 'Female', value: 'female' },
  ];

  MaritalStatusList: { label: string; value: string }[] = [
    { label: 'Single', value: 'single' },
    { label: 'Married', value: 'married' },
    { label: 'Divorced', value: 'divorced' },
    { label: 'Widowed', value: 'widowed' },
  ];

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private messageService: MessageService,
    private leadsService: LeadsService,
    private spinner: NgxSpinnerService,
    private cookieService: CookieService,
  ) {
    this.LeadGenerateForm = this.fb.group({
      FamilyNumber: [''],
      HouseUnitNumber: ['', Validators.required],
      SubNumber: [''],
      HouseholdNo: [''],

      FullName: ['', Validators.required],
      selectedGender: ['', Validators.required],
      selectedMaritalStatus: ['', Validators.required],
      Mobile: ['', Validators.required],
      Job: ['', Validators.required],
      Income: [null, Validators.required],
      Birthday: ['', Validators.required],
      Nic: ['', Validators.required],

      Address: [''],
      Email: [''],

      AidSelections: this.fb.control([]),
      OtherAid: this.fb.control(''),
      FamilyMembers: this.fb.array([]),
      FamilyChilds: this.fb.array([]),
    });
  }

  get familyNumberControl() {
    return this.LeadGenerateForm.get('FamilyNumber');
  }
  get houseUnitNumberControl() {
    return this.LeadGenerateForm.get('HouseUnitNumber');
  }
  get subNumberControl() {
    return this.LeadGenerateForm.get('SubNumber');
  }
  get householdNoControl() {
    return this.LeadGenerateForm.get('HouseholdNo');
  }
  get fullnameControl() {
    return this.LeadGenerateForm.get('FullName');
  }
  get selectedGenderControl() {
    return this.LeadGenerateForm.get('selectedGender');
  }
  get selectedMaritalStatus() {
    return this.LeadGenerateForm.get('selectedMaritalStatus');
  }
  get emailControl() {
    return this.LeadGenerateForm.get('Email');
  }
  get mobileControl() {
    return this.LeadGenerateForm.get('Mobile');
  }
  get nicControl() {
    return this.LeadGenerateForm.get('Nic');
  }
  get addressControl() {
    return this.LeadGenerateForm.get('Address');
  }
  get jobControl() {
    return this.LeadGenerateForm.get('Job');
  }
  get incomeControl() {
    return this.LeadGenerateForm.get('Income');
  }
  get birthdayControl() {
    return this.LeadGenerateForm.get('Birthday');
  }

  get aidSelections(): FormControl {
    return this.LeadGenerateForm.get('AidSelections') as FormControl;
  }

  get familyMembers(): FormArray {
    return this.LeadGenerateForm.get('FamilyMembers') as FormArray;
  }

  getFamilyMemberAidSelections(index: number): FormControl {
    return this.familyMembers.at(index).get('AidSelections') as FormControl;
  }

  getFamilyMemberOtherAid(index: number): FormControl {
    return this.familyMembers.at(index).get('OtherAid') as FormControl;
  }

  get familyChilds(): FormArray {
    return this.LeadGenerateForm.get('FamilyChilds') as FormArray;
  }

  getFamilyChildAidSelections(index: number): FormControl {
    return this.familyChilds.at(index).get('AidSelections') as FormControl;
  }

  getFamilyChildOtherAid(index: number): FormControl {
    return this.familyChilds.at(index).get('OtherAid') as FormControl;
  }

  get isStepValid(): boolean {
    return this.LeadGenerateForm.valid;
  }
  
  async ngOnInit() {
    if (this.stepper) {
      if (!this.isStepValid) {
        this.stepper.linear = true;
      }
    }
  }

  isStepsValid() {
    this.isStepValid;
    if (this.stepper) {
      if (this.isStepValid) {
        this.stepper.linear = false;
      } else {
        this.stepper.linear = true;
      }
    }
  }

  addFamilyMember() {
    this.familyMembers.push(
      this.fb.group({
        FullName: ['', Validators.required],
        Nic: ['', Validators.required],
        Birthday: ['', Validators.required],
        selectedGender: ['', Validators.required],
        selectedMaritalStatus: ['', Validators.required],
        Email: [''],
        Job: ['', Validators.required],
        Mobile: [''],
        Income: [null, Validators.required],
        AidSelections: this.fb.control([]),
        OtherAid: this.fb.control(''),
      }),
    );
  }

  addChildMember() {
    this.familyChilds.push(
      this.fb.group({
        FullName: ['', Validators.required],
        Nic: [''],
        Birthday: ['', Validators.required],
        selectedGender: ['', Validators.required],
        AidSelections: this.fb.control([]),
        OtherAid: this.fb.control(''),
      }),
    );
  }

  deleteFamilyMember(index: number) {
    this.familyMembers.removeAt(index);
  }

  deleteChildMember(index: number) {
    this.familyChilds.removeAt(index);
  }

  mapAidSelections(selected: string[], otherDetails: string): AidSelections {
    return {
      Special: selected.includes('Special'),
      Kidney: selected.includes('Kidney'),
      Health: selected.includes('Health'),
      Scholarship: selected.includes('Scholarship'),
      Disability: selected.includes('Disability'),
      Aswesuma_1: selected.includes('Aswesuma_1'),
      Aswesuma_2: selected.includes('Aswesuma_2'),
      Aswesuma_3: selected.includes('Aswesuma_3'),
      Aswesuma_4: selected.includes('Aswesuma_4'),
      Aswesuma_5: selected.includes('Aswesuma_5'),
      Aswesuma_6: selected.includes('Aswesuma_6'),
      Elders: selected.includes('Elders'),
      Others: selected.includes('Other'),
      OthersDetails: selected.includes('Other') ? otherDetails || '' : '',
    };
  }

  submit() {
    const formValue = this.LeadGenerateForm.value;
    this.leadData.FamilyNumber = formValue.FamilyNumber;
    this.leadData.HouseUnitNumber = formValue.HouseUnitNumber;
    this.leadData.SubNumber = formValue.SubNumber;
    this.leadData.HouseholdNo = formValue.HouseholdNo;
    this.leadData.FullName = formValue.FullName;
    this.leadData.Mobile = formValue.Mobile;
    this.leadData.Nic = formValue.Nic;
    this.leadData.Gender = this.dropdownLabel(formValue.selectedGender);
    this.leadData.MaritalStatus = this.dropdownLabel(formValue.selectedMaritalStatus);
    this.leadData.Job = formValue.Job;
    this.leadData.Income =
      formValue.Income != null && formValue.Income !== ''
        ? String(formValue.Income)
        : '';
    this.leadData.Birthday = this.formatSummaryDate(formValue.Birthday);
  }

  /** Readable label from p-dropdown value `{ label, value }` or plain string. */
  dropdownLabel(value: { label?: string; value?: string } | string | null | undefined): string {
    if (value == null || value === '') {
      return 'NA';
    }
    if (typeof value === 'object' && 'label' in value && value.label) {
      return value.label;
    }
    if (typeof value === 'string') {
      return value;
    }
    return 'NA';
  }

  formatSummaryDate(value: unknown): string {
    if (value == null || value === '') {
      return 'NA';
    }
    if (value instanceof Date) {
      return value.toLocaleDateString();
    }
    return String(value);
  }

  formatAidSelections(selected: string[] | null | undefined): string {
    if (!selected?.length) {
      return 'None selected';
    }
    return selected.join(', ');
  }

  async SubmitLeadForm() {
    const formValue = this.LeadGenerateForm.value;

    const newLeads = new leadData();
    newLeads.GramaOfficeCode = this.cookieService.get('e_gramaCode');
    newLeads.OfficerId = this.cookieService.get('e_userid');
    newLeads.FamilyNumber = this.cookieService.get('e_gramaCode'); // grama code
    newLeads.HouseUnitNumber = formValue.HouseUnitNumber; // index
    newLeads.SubNumber = formValue.SubNumber; //House hold number
    //newLeads.HouseholdNo = formValue.HouseholdNo; // Family Count
    newLeads.HouseholdNo = (this.familyMembers.value?.length || 0) + (this.familyChilds.value?.length || 0);
    newLeads.FullName = formValue.FullName;
    newLeads.Gender = formValue.selectedGender?.value || '';
    newLeads.MaritalStatus = formValue.selectedMaritalStatus?.value || '';
    newLeads.Mobile = formValue.Mobile;
    newLeads.Job = formValue.Job;
    newLeads.Income = formValue.Income;
    newLeads.Birthday = formValue.Birthday;
    newLeads.Nic = formValue.Nic;

    newLeads.AidSelections = [
      this.mapAidSelections(formValue.AidSelections, formValue.OtherAid),
    ];

    newLeads.FamilyMember = this.familyMembers.value.map((member: any) => ({
      FullName: member.FullName,
      Nic: member.Nic,
      Birthday: member.Birthday,
      MaritalStatus: member.selectedMaritalStatus?.value || '',
      Gender: member.selectedGender?.value || '',
      Email: member.Email,
      Job: member.Job,
      Mobile: member.Mobile,
      Income: member.Income,
      AidSelections: [
        this.mapAidSelections(member.AidSelections, member.OtherAid),
      ],
    }));

    newLeads.FamilyChild = this.familyChilds.value.map((child: any) => ({
      FullName: child.FullName,
      Nic: child.Nic,
      Birthday: child.Birthday,
      Gender: child.selectedGender?.value || '',
      AidSelections: [
        this.mapAidSelections(child.AidSelections, child.OtherAid),
      ],
    }));

    this.spinner.show();
    const resp = await this.leadsService.SaveLeads(newLeads);
    if (resp) {
      this.messageService.add({
        severity: 'success',
        summary: 'Success',
        detail: 'Data Save Successfully',
      });
      setTimeout(() => {
        this.resetStepper();
        this.ResetAll();
        this.router.navigate(['/dashboard']);
      }, 1);
      this.spinner.hide();
    } else {
      this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: 'Data Save Failed',
      });
      this.spinner.hide();
    }
  }

  resetStepper() {
    if (this.stepper) {
      this.stepper.activeStep = 0;
    }
  }

  ResetAll() {
    this.LeadForm = new leadData();
    this.LeadGenerateForm.reset();
    this.selectedCompanies = [];
    this.selectedProducts = {};
    this.productDetails = {};
  }
}
