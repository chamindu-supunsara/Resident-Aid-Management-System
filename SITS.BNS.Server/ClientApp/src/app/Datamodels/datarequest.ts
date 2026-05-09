export class CompanyList {
    ID: number = 0;
    name: string = '';
    code: string = '';
    products: Product[] = [];
}

export class Companies {
    id: number = 0;
    name: string = '';
}

export class Product {
    id: number = 0;
    code: string = '';
    name: string = '';
}

export class ViewLeadsFilter {
    id: number = 0;
    userId: number = 0;
    category: string = '';
    action: string = '';
    editedBy: string = '';
    createdDate: string = '';
    updatedDate: string = '';
}

export class AuditsLogs {
    id: number = 0;
    lpid: number = 0;
    refno: string = '';
    fullname: string = '';
    email: string = '';
    mobile: string = '';
    address: string = '';
    status: string = '';
}

export class ViewFamilyMembersFilter {
    id: number = 0;
    familyNo: number = 0;
    houseUnitNo: string = '';
    subNo: string = '';
    householdNo: string = '';
    wasama: string = '';
    wasamNo: string = '';
    officerName: string = '';
}

export class ViewOfficersFilter {
    id: number = 0;
    lastname: string = '';
    mobile: string = '';
    wasamName: string = '';
    firstname: string = '';
    userEmail: string = '';
    status: boolean = false;
}

export class ViewAIDFilter {
    id: number = 0;
    age: number = 0;
    familyNo: string = '';
    houseUnitNo: string = '';
    subNo: string = '';
    householdNo: string = '';
    fullName: string = '';
    nic: string = '';
    gramaCode : string = '';
    aidsList: string[] = [];
}

export class ViewLeadSubmitterData {
    id: number = 0;
    fullName: string = '';
    organization: string = '';
    mobile: string = '';
    date: string = '';
    time: string = '';
}

export class FamilyMembers {
    ID: number = 0;
    Fullname: string = '';
    NIC: string = '';
    Birthday: string = '';
    Gender: string = '';
    MaritalStatus: string = '';
    Job: string = '';
    Mobile: string = '';
    Income: string = '';
}

export class leadData {
    ID: number = 0;
    GramaOfficeCode: string = '';
    OfficerId: string = '';
    FamilyNumber: string = '';
    HouseUnitNumber: string = '';
    SubNumber: string = '';
    HouseholdNo: string = '';
    FullName: string = '';
    Gender: string = '';
    MaritalStatus: string = '';
    Mobile: string = '';
    Job: string = '';
    Income: string = '';
    Birthday: string = '';
    Nic: string = '';
    AidSelections: AidSelections[] = [];
    FamilyMember: FamilyMember[] = [];
    FamilyChild: FamilyChild[] = [];
}

export class FamilyMember {
    FullName: string = '';
    Nic: string = '';
    Birthday: string = '';
    MaritalStatus: string = '';
    Gender: string = '';
    Job: string = '';
    Mobile: string = '';
    Income: string = '';
    AidSelections: AidSelections[] = [];
}

export class FamilyChild {
    FullName: string = '';
    Nic: string = '';
    Birthday: string = '';
    Gender: string = '';
    AidSelections: AidSelections[] = [];
}

export class AidSelections {
    Special: boolean = false;
    Kidney: boolean = false;
    Health: boolean = false;
    Scholarship: boolean = false;
    Disability: boolean = false;
    Aswesuma_1: boolean = false;
    Aswesuma_2: boolean = false;
    Aswesuma_3: boolean = false;
    Aswesuma_4: boolean = false;
    Aswesuma_5: boolean = false;
    Aswesuma_6: boolean = false;
    Elders: boolean = false;
    Others: boolean = false;
    OthersDetails: string = '';
}

export class AidSelectionsDTO {
    Special: boolean = false;
    Kidney: boolean = false;
    Health: boolean = false;
    Scholarship: boolean = false;
    Disability: boolean = false;
    Aswesuma_1: boolean = false;
    Aswesuma_2: boolean = false;
    Aswesuma_3: boolean = false;
    Aswesuma_4: boolean = false;
    Aswesuma_5: boolean = false;
    Aswesuma_6: boolean = false;
    Elders: boolean = false;
    Others: boolean = false;
}

export class ViewLeadData {
    id: number = 0;
    firstname: string = '';
    lastname: string = '';
    email: string = '';
    mobile: string = '';
    nic: string = '';
    address: string = '';
    leadsList: Lead[] = [];
    productsList: ProductList[] = [];
}

export class ViewFamilyData {
    id: number = 0;
    familyNo: string = '';
    houseUnitNo: string = '';
    subNo: string = '';
    householdNo: string = '';
    wasama: string = '';
    wasamNo: string = '';
    officerName: string = '';
    familyMembers: FMembersDto[] = [];
}

export class FMembersDto {
    id: number = 0;
    fullName: string = '';
    nic: string = '';
    birthday: string = '';
    maritalStatus: string = '';
    gender: string = '';
    job: string = '';
    mobile: string = '';
    income: number = 0;
    aidsList: string[] = [];
    isChild: boolean = false;
}

export class ViewMemberData {
    id: number = 0;
    fullName: string = '';
    nic: string = '';
    birthDay: string = '';
    gender: string = '';
    maritalStatus: string = '';
    job: string = '';
    phoneNumber: string = '';
    income: number = 0;
    familyID: number = 0;

    specialAids: boolean = false;
    other: boolean = false;
    aswesuma_1: boolean = false;
    aswesuma_2: boolean = false;
    aswesuma_3: boolean = false;
    aswesuma_4: boolean = false;
    aswesuma_5: boolean = false;
    aswesuma_6: boolean = false;
    kidney: boolean = false;
    scholarship: boolean = false;
    elders: boolean = false;
    health: boolean = false;
    disability: boolean = false;

    others: string = '';
}

export class Lead {
    refNo: string = '';
    status: string = '';
    companyCode: string = '';
    remark: string = '';
}

export class ProductList {
    productName: string = '';
    productCode: string = '';
    amount: string = '';
    remark: string = '';
    measurement: string = '';
}

export class StatusList {
    name: string = '';
    code: string = '';
}

export interface BranchLocation {
    id: number;
    name: string;
    latitude: number;
    longitude: number;
    mobile: string;
}
