import { Component, OnInit } from '@angular/core';
import { LeadsService } from '../../services/common.service';
import { CookieService } from 'ngx-cookie-service';
import {
  Lead,
  ProductList,
  ViewFamilyData,
  ViewFamilyMembersFilter,
  ViewLeadsFilter,
  ViewLeadSubmitterData,
  FMembersDto,
  ViewMemberData,
} from '../../Datamodels/datarequest';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageService } from 'primeng/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddMemberForm, UpdateMemberForm } from '../../Datamodels/daraforms';

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
  MemberForm: FormGroup;

  memberDialogVisible = false;
  memberDialogMode: 'add' | 'edit' = 'add';
  memberDialogIsChild = false;

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

    this.MemberForm = this.fb.group({
      ID: [0],
      FullName: ['', Validators.required],
      Nic: [''],
      Birthday: [null as Date | null, Validators.required],
      selectedGender: [null as { label: string; value: string } | null, Validators.required],
      selectedMaritalStatus: [null as { label: string; value: string } | null],
      Job: [''],
      Mobile: [''],
      Income: [''],
      Special: [false],
      Kidney: [false],
      Health: [false],
      Scholarship: [false],
      Disability: [false],
      Aswesuma_1: [false],
      Aswesuma_2: [false],
      Aswesuma_3: [false],
      Aswesuma_4: [false],
      Aswesuma_5: [false],
      Aswesuma_6: [false],
      Elders: [false],
      Others: [false],
      OthersDetails: [''],
    });
  }

  get memberDialogHeader(): string {
    if (this.memberDialogMode === 'edit') {
      return this.memberDialogIsChild ? 'Edit child' : 'Edit family member';
    }
    return this.memberDialogIsChild ? 'Add child' : 'Add family member';
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

  reloadHouseMembers() {
    this.leadsService.GetHousebyID(this.HouseID).subscribe((resp) => {
      if (resp) {
        this.MembersList = resp;
        this.HouseForm.patchValue({
          HouseholdNo: this.MembersList.householdNo,
        });
      }
    });
  }

  applyAdultMemberValidators() {
    this.MemberForm.get('Nic')?.setValidators([Validators.required]);
    this.MemberForm.get('selectedMaritalStatus')?.setValidators([Validators.required]);
    this.MemberForm.get('Job')?.setValidators([Validators.required]);
    this.MemberForm.get('Mobile')?.setValidators([Validators.required]);
    this.MemberForm.get('Income')?.setValidators([Validators.required]);
    ['Nic', 'selectedMaritalStatus', 'Job', 'Mobile', 'Income'].forEach((k) => {
      this.MemberForm.get(k)?.updateValueAndValidity({ emitEvent: false });
    });
  }

  clearAdultMemberValidators() {
    ['Nic', 'selectedMaritalStatus', 'Job', 'Mobile', 'Income'].forEach((k) => {
      this.MemberForm.get(k)?.clearValidators();
      this.MemberForm.get(k)?.updateValueAndValidity({ emitEvent: false });
    });
  }

  openAddMember(isChild: boolean) {
    this.memberDialogMode = 'add';
    this.memberDialogIsChild = isChild;
    this.MemberForm.reset({
      ID: 0,
      FullName: '',
      Nic: '',
      Birthday: null,
      selectedGender: null,
      selectedMaritalStatus: null,
      Job: '',
      Mobile: '',
      Income: '',
      Special: false,
      Kidney: false,
      Health: false,
      Scholarship: false,
      Disability: false,
      Aswesuma_1: false,
      Aswesuma_2: false,
      Aswesuma_3: false,
      Aswesuma_4: false,
      Aswesuma_5: false,
      Aswesuma_6: false,
      Elders: false,
      Others: false,
      OthersDetails: '',
    });
    if (isChild) {
      this.clearAdultMemberValidators();
    } else {
      this.applyAdultMemberValidators();
    }
    this.memberDialogVisible = true;
  }

  openEditMember(member: FMembersDto) {
    this.memberDialogMode = 'edit';
    this.memberDialogIsChild = member.isChild;
    this.spinner.show();
    this.leadsService.GetMemberbyID(member.id).subscribe({
      next: (data: ViewMemberData) => {
        this.patchMemberForm(data);
        if (this.memberDialogIsChild) {
          this.clearAdultMemberValidators();
        } else {
          this.applyAdultMemberValidators();
        }
        this.memberDialogVisible = true;
        this.spinner.hide();
      },
      error: () => {
        this.spinner.hide();
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Could not load member details',
        });
      },
    });
  }

  optionFromValue(
    list: { label: string; value: string }[],
    value: string | null | undefined,
  ): { label: string; value: string } | null {
    if (value == null || value === '') {
      return null;
    }
    const found = list.find((o) => o.value === value);
    return found ?? { label: value, value };
  }

  patchMemberForm(data: ViewMemberData) {
    const inc = data.income != null && data.income !== undefined ? String(data.income) : '';
    this.MemberForm.patchValue({
      ID: data.id,
      FullName: data.fullName,
      Nic: data.nic,
      Birthday: data.birthDay ? new Date(data.birthDay) : null,
      selectedGender: this.optionFromValue(this.GenderList, data.gender),
      selectedMaritalStatus: this.optionFromValue(this.MaritalStatusList, data.maritalStatus),
      Job: data.job,
      Mobile: data.phoneNumber,
      Income: inc,
      Special: !!data.specialAids,
      Kidney: !!data.kidney,
      Health: !!data.health,
      Scholarship: !!data.scholarship,
      Disability: !!data.disability,
      Aswesuma_1: !!data.aswesuma_1,
      Aswesuma_2: !!data.aswesuma_2,
      Aswesuma_3: !!data.aswesuma_3,
      Aswesuma_4: !!data.aswesuma_4,
      Aswesuma_5: !!data.aswesuma_5,
      Aswesuma_6: !!data.aswesuma_6,
      Elders: !!data.elders,
      Others: !!data.other,
      OthersDetails: data.others ?? '',
    });
  }

  private formatBirthdayForApi(v: Date | string | null | undefined): string {
    if (v == null) {
      return '';
    }
    if (v instanceof Date) {
      return v.toISOString().split('T')[0];
    }
    return String(v);
  }

  memberFormToUpdateDto(): UpdateMemberForm {
    const v = this.MemberForm.getRawValue();
    const isChild = this.memberDialogIsChild;
    const dto = new UpdateMemberForm();
    dto.ID = v.ID;
    dto.FullName = v.FullName;
    dto.Nic = isChild ? (v.Nic || '') : v.Nic;
    dto.Birthday = this.formatBirthdayForApi(v.Birthday);
    dto.MaritalStatus = isChild ? '' : (v.selectedMaritalStatus?.value ?? '');
    dto.Gender = v.selectedGender?.value ?? '';
    dto.Job = isChild ? '' : (v.Job ?? '');
    dto.Mobile = isChild ? '' : (v.Mobile ?? '');
    dto.Income = isChild ? '0' : String(v.Income ?? '');
    dto.Special = !!v.Special;
    dto.Kidney = !!v.Kidney;
    dto.Health = !!v.Health;
    dto.Scholarship = !!v.Scholarship;
    dto.Disability = !!v.Disability;
    dto.Aswesuma_1 = !!v.Aswesuma_1;
    dto.Aswesuma_2 = !!v.Aswesuma_2;
    dto.Aswesuma_3 = !!v.Aswesuma_3;
    dto.Aswesuma_4 = !!v.Aswesuma_4;
    dto.Aswesuma_5 = !!v.Aswesuma_5;
    dto.Aswesuma_6 = !!v.Aswesuma_6;
    dto.Elders = !!v.Elders;
    dto.Others = !!v.Others;
    dto.OthersDetails = v.Others ? (v.OthersDetails ?? '') : '';
    return dto;
  }

  memberFormToAddDto(): AddMemberForm {
    const v = this.MemberForm.getRawValue();
    const isChild = this.memberDialogIsChild;
    const dto = new AddMemberForm();
    dto.FamilyId = this.HouseID;
    dto.IsChild = isChild;
    dto.FullName = v.FullName;
    dto.Nic = v.Nic || '';
    dto.Birthday = this.formatBirthdayForApi(v.Birthday);
    dto.MaritalStatus = isChild ? '' : (v.selectedMaritalStatus?.value ?? '');
    dto.Gender = v.selectedGender?.value ?? '';
    dto.Job = isChild ? '' : (v.Job ?? '');
    dto.Mobile = isChild ? '' : (v.Mobile ?? '');
    dto.Income = isChild ? '0' : String(v.Income ?? '');
    dto.Special = !!v.Special;
    dto.Kidney = !!v.Kidney;
    dto.Health = !!v.Health;
    dto.Scholarship = !!v.Scholarship;
    dto.Disability = !!v.Disability;
    dto.Aswesuma_1 = !!v.Aswesuma_1;
    dto.Aswesuma_2 = !!v.Aswesuma_2;
    dto.Aswesuma_3 = !!v.Aswesuma_3;
    dto.Aswesuma_4 = !!v.Aswesuma_4;
    dto.Aswesuma_5 = !!v.Aswesuma_5;
    dto.Aswesuma_6 = !!v.Aswesuma_6;
    dto.Elders = !!v.Elders;
    dto.Others = !!v.Others;
    dto.OthersDetails = v.Others ? (v.OthersDetails ?? '') : '';
    return dto;
  }

  saveMemberDialog() {
    this.MemberForm.markAllAsTouched();
    if (this.MemberForm.invalid) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Validation',
        detail: 'Please complete all required fields',
      });
      return;
    }

    if (this.memberDialogMode === 'edit') {
      this.leadsService
        .UpdateMember(this.memberFormToUpdateDto())
        .then((resp) => {
          if (resp && resp !== 0) {
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Member updated successfully',
            });
            this.memberDialogVisible = false;
            this.reloadHouseMembers();
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'Failed to update member',
            });
          }
        })
        .catch(() => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Something went wrong',
          });
        });
    } else {
      this.leadsService
        .AddMember(this.memberFormToAddDto())
        .then((resp) => {
          if (resp && resp !== 0) {
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Member added successfully',
            });
            this.memberDialogVisible = false;
            this.reloadHouseMembers();
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'Failed to add member',
            });
          }
        })
        .catch(() => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Something went wrong',
          });
        });
    }
  }

  closeMemberDialog() {
    this.memberDialogVisible = false;
    this.MemberForm.markAsPristine();
    this.MemberForm.markAsUntouched();
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

  DeleteMember(member: FMembersDto) {
    const memberData = { ID: member.id };

    this.leadsService.DeleteMember(memberData).then((resp) => {
      if (resp) {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Member Deleted Successfully',
        });
        this.reloadHouseMembers();
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
