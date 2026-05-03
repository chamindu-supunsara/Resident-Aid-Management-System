import { Component, OnInit, ViewChild } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageService } from 'primeng/api';
import { LeadsService } from '../../services/common.service';
import {
  Lead,
  ProductList,
  ViewAIDFilter,
  ViewLeadData,
  ViewLeadsFilter,
  ViewLeadSubmitterData,
  ViewMemberData,
} from '../../Datamodels/datarequest';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UpdateMemberForm } from '../../Datamodels/daraforms';
import { FilterService } from 'primeng/api';
import * as XLSX from 'xlsx';
import FileSaver from 'file-saver';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrl: './members.component.css',
})
export class MembersComponent implements OnInit {
  
  @ViewChild('dt') dt!: Table;
  ViewLeads: ViewLeadsFilter[] = [];
  ViewAIDFilter: ViewAIDFilter[] = [];
  FilteredUserList: ViewAIDFilter[] = [];
  LeadsList: Lead[] = [];
  PrductList: ProductList[] = [];
  selectedStatus: { name: string; code: string } | null = null;

  ViewLeadsData: ViewLeadData = new ViewLeadData();
  HouseList: ViewMemberData = new ViewMemberData();
  ViewLeadsSubmitterData: ViewLeadSubmitterData = new ViewLeadSubmitterData();

  UserId: string = '';
  LPID: string = '';
  MemberID: number = 0;
  LeadRefNo: string = '';
  LeadStatus: string = '';
  UserRole: string = '';
  Remark: string = '';
  searchText: string = '';

  MemberForm: FormGroup;

  metaKey: boolean = true;
  visible: boolean = false;

  StatusList = [
    { name: 'Accept', code: 'Accept' },
    { name: 'Reject', code: 'Reject' },
  ];

  AidsList = [
    { value: 'Special', label: 'Special' },
    { value: 'Kidney', label: 'Kidney' },
    { value: 'Health', label: 'Health' },
    { value: 'Scholarship', label: 'Scholarship' },
    { value: 'Disability', label: 'Disability' },
    { value: 'Aswesuma Rs 15000', label: 'Aswesuma Rs 15000' },
    { value: 'Aswesuma Rs 10000', label: 'Aswesuma Rs 10000' },
    {
      value: 'Aswesuma Vulnerable Rs 5000',
      label: 'Aswesuma Vulnerable Rs 5000',
    },
    {
      value: 'Aswesuma Transirnt Rs 5000',
      label: 'Aswesuma Transirnt Rs 5000',
    },
    { value: 'Aswesuma Appeal', label: 'Aswesuma Appeal' },
    { value: 'Aswesuma Cycle 2', label: 'Aswesuma Cycle 2' },
    { value: 'Elders', label: 'Elders' },
    { value: 'Other', label: 'Other' },
    { value: 'NA', label: 'N/A' },
  ];

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
    private leadsService: LeadsService,
    private cookieService: CookieService,
    private spinner: NgxSpinnerService,
    private messageService: MessageService,
    private fb: FormBuilder,
    private filterService: FilterService,
  ) {
    this.filterService.register(
      'arrayIncludes',
      (value: string[], filters: string[]): boolean => {
        if (!filters || filters.length === 0) return true;
        const filterNA = filters.includes('NA');
        const nonNAFilters = filters.filter((f) => f !== 'NA');
        const isEmpty = !value || value.length === 0;
        if (filterNA && isEmpty) return true;
        if (!Array.isArray(value)) return false;
        return nonNAFilters.some((f: string) => value.includes(f));
      },
    );

    this.MemberForm = this.fb.group({
      FullName: ['', Validators.required],
      selectedGender: ['', Validators.required],
      selectedMaritalStatus: [''],
      Mobile: [''],
      Job: [''],
      Income: [null],
      Birthday: ['', Validators.required],
      Nic: [''],
    });
  }

  get fullnameControl() {
    return this.MemberForm.get('FullName');
  }
  get selectedGenderControl() {
    return this.MemberForm.get('selectedGender');
  }
  get selectedMaritalStatus() {
    return this.MemberForm.get('selectedMaritalStatus');
  }
  get mobileControl() {
    return this.MemberForm.get('Mobile');
  }
  get jobControl() {
    return this.MemberForm.get('Job');
  }
  get incomeControl() {
    return this.MemberForm.get('Income');
  }
  get birthdayControl() {
    return this.MemberForm.get('Birthday');
  }
  get nicControl() {
    return this.MemberForm.get('Nic');
  }

  get isStepValid(): boolean {
    return this.MemberForm.valid;
  }

  async ngOnInit(): Promise<void> {
    this.spinner.show();
    await this.GetAllAids();
    this.spinner.hide();
  }

  async GetAllAids() {
    this.UserId = this.cookieService.get('e_userid');
    this.UserRole = this.cookieService.get('e_role');
    const reqParams = { UserId: this.UserId, UserRole: this.UserRole };

    this.leadsService.GetAllAids(reqParams).subscribe((resp) => {
      if (resp) {
        this.ViewAIDFilter = resp;
        this.FilteredUserList = [...this.ViewAIDFilter];
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
    this.spinner.show();
    this.MemberID = event.data.id;

    this.visible = true;
    this.ResetForm();

    this.leadsService.GetMemberbyID(this.MemberID).subscribe((resp) => {
      if (resp) {
        this.HouseList = resp;
        this.MemberForm.patchValue({
          FullName: resp.fullName,
          Birthday: new Date(resp.birthDay),
          selectedGender: this.GenderList.find((g) => g.value === resp.gender),
          selectedMaritalStatus: this.MaritalStatusList.find(
            (ms) => ms.value === resp.maritalStatus,
          ),
          Nic: resp.nic,
          Job: resp.job,
          Mobile: resp.phoneNumber,
          Income: resp.income,
        });
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

  ResetForm() {
    this.selectedStatus = null;
    this.Remark = '';
    this.MemberForm.reset();
  }

  UpdateHouse() {
    if (this.MemberForm.valid) {
      const formValue = this.MemberForm.value;
      const memberData: UpdateMemberForm = {
        ID: this.MemberID,
        FullName: formValue.FullName,
        Nic: formValue.Nic || null,
        Birthday: formValue.Birthday,
        MaritalStatus: formValue.selectedMaritalStatus?.value || null,
        Gender: formValue.selectedGender?.value || null,
        Job: formValue.Job || null,
        Mobile: formValue.Mobile || null,
        Income: formValue.Income,

        Special: this.HouseList.specialAids,
        Kidney: this.HouseList.kidney,
        Health: this.HouseList.health,
        Scholarship: this.HouseList.scholarship,
        Disability: this.HouseList.disability,
        Aswesuma_1: this.HouseList.aswesuma_1,
        Aswesuma_2: this.HouseList.aswesuma_2,
        Aswesuma_3: this.HouseList.aswesuma_3,
        Aswesuma_4: this.HouseList.aswesuma_4,
        Aswesuma_5: this.HouseList.aswesuma_5,
        Aswesuma_6: this.HouseList.aswesuma_6,
        Elders: this.HouseList.elders,
        Others: this.HouseList.other,

        OthersDetails: this.HouseList.others,
      };

      this.leadsService.UpdateMember(memberData).then(async (resp) => {
        if (resp) {
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'House Updated Successfully',
          });
          this.visible = false;
          await this.GetAllAids();
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Failed to Update House',
          });
        }
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
    const memberData = { ID: this.MemberID };
    this.leadsService.DeleteMember(memberData).then(async (resp) => {
      if (resp) {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'House Deleted Successfully',
        });
        this.MemberForm.reset();
        await this.GetAllAids();
        this.visible = false;
      } else {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to Delete House',
        });
      }
    });
  }

  ClearSearch() {
    this.searchText = '';
    this.FilteredUserList = [...this.ViewAIDFilter];
  }

  filterProducts() {
    const search = this.searchText.toLowerCase();

    if (!search) {
      this.FilteredUserList = [...this.ViewAIDFilter];
      return;
    }

    this.FilteredUserList = this.ViewAIDFilter.filter((users) =>
      users.age.toString().toLowerCase().includes(search),
    );
  }

  DownloadExcel(dt: Table) {
    let exportData = dt.filteredValue
      ? dt.filteredValue
      : this.FilteredUserList;

    if (!exportData || exportData.length === 0) {
      return;
    }

    const dataToExport = exportData.map((item) => ({
      'Full Name': item.fullName,
      NIC: item.nic,
      'Wasam No': item.gramaCode,
      'Village No': item.familyNo,
      'Index No': item.houseUnitNo,
      'House Hold No': item.subNo,
      'Total Family Members': item.householdNo,
      'AIDs List':
        item.aidsList && item.aidsList.length > 0
          ? item.aidsList.join(', ')
          : 'N/A',
    }));

    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(dataToExport);
    const workbook: XLSX.WorkBook = {
      Sheets: { 'AID Data': worksheet },
      SheetNames: ['AID Data'],
    };

    const excelBuffer: any = XLSX.write(workbook, {
      bookType: 'xlsx',
      type: 'array',
    });

    const blob: Blob = new Blob([excelBuffer], {
      type: 'application/octet-stream',
    });

    FileSaver.saveAs(blob, 'AID_Data.xlsx');
  }

  DownloadPDF(dt: Table) {
    let exportData = dt.filteredValue
      ? dt.filteredValue
      : this.FilteredUserList;

    if (!exportData || exportData.length === 0) {
      return;
    }

    const doc = new jsPDF();
    const rows = exportData.map((item) => [
      item.fullName,
      item.nic,
      item.gramaCode,
      item.familyNo,
      item.houseUnitNo,
      item.subNo,
      item.householdNo,
      item.aidsList && item.aidsList.length > 0
        ? item.aidsList.join(', ')
        : 'N/A',
    ]);

    const headers = [
      [
        'Full Name',
        'NIC',
        'Wasam No',
        'Village No',
        'Index No',
        'House Hold No',
        'Total Family Members',
        'AIDs List',
      ],
    ];

    autoTable(doc, {
      head: headers,
      body: rows,
      styles: { fontSize: 8 },
      headStyles: { fillColor: [41, 128, 185] },
      startY: 20,
    });

    doc.save('AID_Data.pdf');
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
        return 'contrast';
    }
  }
}
