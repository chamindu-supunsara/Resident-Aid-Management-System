import { Component, Inject, NgZone, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AuthOTPRequest, AuthRequest, ResetPassword, UserForm } from '../../Datamodels/daraforms';
import { LoginService } from '../../services/login.service';
import { CookieService } from 'ngx-cookie-service';
import { MessageService } from 'primeng/api';
import { NgxSpinnerService } from 'ngx-spinner';
import { Companies } from '../../Datamodels/datarequest';
import {  FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LeadsService } from '../../services/common.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  UserId: string = '';
  UserEmail: string = '';
  UserPassword: string = '';
  visible: boolean = false;
  emailExists: boolean = false;
  PRemailExists: boolean = false;
  formData: AuthRequest = new AuthRequest();
  formDataOtp: AuthOTPRequest = new AuthOTPRequest();
  UserForm: UserForm = new UserForm();
  Companies: Companies[] = [];
  RegisterForm: FormGroup;
  ResetForm: FormGroup;

  constructor(
    private router: Router, 
    private loginService: LoginService, 
    private cookieService: CookieService,
    private messageService: MessageService,
    private leadsService: LeadsService,
    private spinner: NgxSpinnerService,
    private fb: FormBuilder,
    private ngZone: NgZone
  ) { 
    
    this.RegisterForm = this.fb.group({
      selectedCompany: ['', Validators.required],
      Email: ['', [Validators.required, Validators.email]],
      Firstname: ['', Validators.required],
      Lastname: ['', Validators.required],
      Mobile: ['', Validators.required],
      Password: ['', Validators.required],
    });

    this.ResetForm = this.fb.group({
      UserName: ['', [Validators.required, Validators.email]],
      NPassword: ['', Validators.required],
      CMPassword: ['', Validators.required],
    });
  }

  get selectedCompanyControl() {
    return this.RegisterForm.get('selectedCompany');
  }
  get EmailControl() {
    return this.RegisterForm.get('Email');
  }
  get FirstnameControl() {
    return this.RegisterForm.get('Firstname');
  }
  get LastnameControl() {
    return this.RegisterForm.get('Lastname');
  }
  get MobileControl() {
    return this.RegisterForm.get('Mobile');
  }
  get PasswordControl() {
    return this.RegisterForm.get('Password');
  }

  get UserNameControl() {
    return this.ResetForm.get('UserName');
  }
  get NPasswordControl() {
    return this.ResetForm.get('NPassword');
  }
  get CMPasswordControl() {
    return this.ResetForm.get('CMPassword');
  }

  get isValid(): boolean {
    return this.RegisterForm.valid;
  }

  get isRPValid(): boolean {
    return this.ResetForm.valid;
  }

  async ngOnInit(): Promise<void> {
    this.spinner.show();
    await this.GetAllCompanys();
    this.spinner.hide();
  }

  OnTabChange(event: any) {
    if (event.index === 0) {
      this.UserForm = new UserForm();
    } else {
      this.UserEmail = '';
      this.UserPassword = '';
    }

    if (event.index === 1) {
      this.UserForm = new UserForm();
      this.RegisterForm.reset();
    }
  }

  showDialog() {
    this.visible = true;
    this.ResetForm.reset();
    this.UserEmail = '';
    this.UserPassword = '';
  }

  async InAppLogin() {
    this.formData = {
      USER_ID: this.UserEmail,
      USER_PASSWORD: this.UserPassword,
      TYPE: "InApp"
    };
    
    this.spinner.show();
    this.loginService.loginUser(this.formData).then((response) => {
      if (response.success) {
        if (response.roles.length > 0) {
          this.cookieService.set("e_name", response.name);
          this.cookieService.set('e_email', response.email);
          this.cookieService.set('e_userid', response.userId);
          this.cookieService.set('e_gramaCode', response.organizationId);
          this.cookieService.set('e_loggedinat', new Date().getTime().toString());
          this.cookieService.set('e_jwt', response.jwToken);
          this.cookieService.set('e_refreshtkn', response.refreshToken);
          this.cookieService.set('e_authtype', response.authType);
          this.cookieService.set('e_role', response.roles[0]);

          var role_codes : string[]= [];
          response.roles.forEach((role: any) => {
            switch (role) {
              case "Admin": {
                role_codes.push("Admin");
                break;
              }
              case "User": {
                role_codes.push("User");
                break;
              }
            }

            this.cookieService.set('e_user_roles', JSON.stringify(role_codes));
          });

          const token = localStorage.getItem('e_jwt');

          setTimeout(() => {this.router.navigate(['/dashboard']);});
          this.spinner.hide();
          
        } else {
          this.messageService.add({ severity: 'warn', summary: 'Warning', detail: 'Invalid Username or Password.' });
          this.router.navigate(['']);
          this.spinner.hide();
        }
      } else {
        this.messageService.add({ severity: 'warn', summary: 'Warning', detail: 'Invalid Username or Password.' });
        this.router.navigate(['']);
        this.spinner.hide();
      }
    });
  }

  InAppSignUp() {
    const email = this.RegisterForm.get('Email')?.value;
    if (email) {
      this.loginService.CheckEmailExists(email).subscribe(async response => {
        this.emailExists = response.emailExists;

        if (this.emailExists === false) {
          const userRegister = new UserForm();
          userRegister.Firstname = this.RegisterForm.get('Firstname')?.value;
          userRegister.Lastname = this.RegisterForm.get('Lastname')?.value;
          userRegister.Email = this.RegisterForm.get('Email')?.value;
          userRegister.Mobile = this.RegisterForm.get('Mobile')?.value;
          userRegister.Password = this.RegisterForm.get('Password')?.value;
          const selectedCompany = this.RegisterForm.get('selectedCompany')?.value;
          if (selectedCompany) {
            userRegister.CompanyID = selectedCompany.id;
            userRegister.CompanyName = selectedCompany.name;
            userRegister.GramaID = selectedCompany.locationCode;
          }
          this.spinner.show();
          const resp = await this.loginService.SaveUsers(userRegister);
          if (resp) {
            this.messageService.add({ severity: 'success', summary: 'Success', detail: 'User Registered Successfully' });
            await this.GetAllCompanys();
            this.RegisterForm.reset();
            this.UserForm = new UserForm();
            this.spinner.hide();
          } else {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'User Registration Failed' });
            this.spinner.hide();
          }
        } else {
          this.messageService.add({ severity: 'warn', summary: 'Warning', detail: 'Email Already Exists' });
          this.spinner.hide();
        }
      });
    }
  }

  ForgetPassword() {
    const email = this.ResetForm.get('UserName')?.value;
    const newPassword = this.ResetForm.get('NPassword')?.value;
    const Conpassword = this.ResetForm.get('CMPassword')?.value;

    if (email) {
      this.loginService.CheckEmailExists(email).subscribe(async response => {
        this.PRemailExists = response.emailExists;

        if (this.PRemailExists === true) {
          if (newPassword === Conpassword) {
            const resetPassword = new ResetPassword();
            resetPassword.UserEmail = this.ResetForm.get('UserName')?.value;
            resetPassword.NewPassword = this.ResetForm.get('NPassword')?.value;

            this.spinner.show();
            const resp = await this.loginService.ResetPassword(resetPassword);
            if (resp) {
              this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Password Reset Successfully' });
              this.Reset();
              this.visible = false;

              this.spinner.hide();
            } else {
              this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Password Reset Failed.' });
              this.spinner.hide();
            }
          } else {
            this.messageService.add({ severity: 'warn', summary: 'Warning', detail: 'New password and confirmation password must match.' });
            this.spinner.hide();
          }
        } else {
          this.messageService.add({ severity: 'warn', summary: 'Warning', detail: 'This Email Not Registered.' });
          this.spinner.hide();
        }
      });
    }
  }

  Reset() {
    this.ResetForm.reset();
    this.UserEmail = '';
    this.UserPassword = '';
  }

  async GetAllCompanys() {
    this.leadsService.GetAllCompanyIsAvailable().subscribe(resp => {
      if (resp) {
        this.Companies = resp;
      } else {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Data Load Failed' });
      }
    });
  }
}
