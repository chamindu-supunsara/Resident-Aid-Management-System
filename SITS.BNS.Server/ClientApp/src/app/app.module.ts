import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ButtonModule } from 'primeng/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FloatLabelModule } from 'primeng/floatlabel';
import { LoginComponent } from './Pages/login/login.component';
import { CardModule } from 'primeng/card';
import { PasswordModule } from 'primeng/password';
import { DialogModule } from 'primeng/dialog';
import { BreadcrumbModule, GridModule, HeaderModule, SidebarModule } from '@coreui/angular';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { MenuModule } from 'primeng/menu';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { TabViewModule } from 'primeng/tabview';
import { NgxSpinnerModule } from 'ngx-spinner';
import { DropdownModule } from 'primeng/dropdown';
import { InputMaskModule } from 'primeng/inputmask';
import { NgOtpInputModule } from 'ng-otp-input';
import { AuthInterceptor } from './AuthInterceptor';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule, 
    HttpClientModule,
    AppRoutingModule,
    ButtonModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    InputTextModule,
    FloatLabelModule,
    CardModule,
    PasswordModule,
    DialogModule,
    GridModule,
    TabViewModule,
    SidebarModule,
    NgScrollbarModule,
    DropdownModule,
    HeaderModule,
    BreadcrumbModule,
    MenuModule,
    ToastModule,
    NgxSpinnerModule,
    ReactiveFormsModule,
    InputMaskModule,
    NgOtpInputModule
  ],
  providers: [MessageService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor, 
      multi: true,
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
