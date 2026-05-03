export class AuthRequest {
    USER_ID!: string | null;
    USER_PASSWORD!: string | null;
    TYPE!: string | null;
}

export class AuthOTPRequest {
    OTP!: string | null;
    USER_ID!: string | null;
}

export class LeadForm {
    ID: number = 0;
    Submitby: string = '';
    Firstname: string = '';
    Lastname: string = '';
    Email: string = '';
    Mobile: string = '';
    Nic: string = '';
    Address: string = '';
    CompanyList: CompanyList[] = [];
}

export class ViewOfficersForm {
    ID: number = 0;
    Lastname: string = '';
    Mobile: string = '';
    Firstname: string = '';
    Email: string = '';
    IsActive: boolean = false;
}

export class UpdateHouseForm {
    ID: number = 0;
    FamilyNo: string = '';
    HouseUnitNo: string = '';
    SubNo: string = '';
    HouseholdNo: string = '';
}

export class UpdateMemberForm {
    ID: number = 0;
    FullName: string = '';
    Nic: string = '';
    Birthday: string = '';
    MaritalStatus: string = '';
    Gender: string = '';
    Job: string = '';
    Mobile: string = '';
    Income: string = '';

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

export class DeleteForm {
    ID: number = 0;
}

export class CompanyList {
    ID: number = 0;
    CompanyCode: string = '';
    ProductList: ProductList[] = [];
}

export class ProductList {
    ID: number = 0;
    productId: number = 0;
    code: string = '';
    name: string = '';
    amount: string = '';
    remark: string = '';
    measurement: string = '';
}

export class LeadStatusForm {
    LPID: string = '';
    Status: string = '';
    Remark: string = '';
}

export class UserForm {
    ID: number = 0;
    Firstname: string = '';
    Lastname: string = '';
    Email: string = '';
    Mobile: string = '';
    CompanyName: string = '';
    CompanyID: number = 0;
    Password: string = '';
    GramaID: number = 0;
}

export class ResetPassword {
    UserEmail: string = '';
    NewPassword: string = '';
}