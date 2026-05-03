import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfficerConfigRoutingModule } from './officer-config-routing.module';
import { OfficerConfigComponent } from './officer-config.component';
import { TableModule } from 'primeng/table';
import { CardModule } from 'primeng/card';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastModule } from 'primeng/toast';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { SkeletonModule } from 'primeng/skeleton';
import { MessageService } from 'primeng/api';
import { FloatLabelModule } from 'primeng/floatlabel';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { InputMaskModule } from 'primeng/inputmask';

@NgModule({
  declarations: [OfficerConfigComponent],
  providers: [MessageService],
  imports: [
    CommonModule,
    OfficerConfigRoutingModule,
    ReactiveFormsModule,
    InputTextModule,
    InputMaskModule,
    FormsModule,
    TableModule,
    CardModule,
    NgxSpinnerModule,
    ButtonModule,
    ToastModule,
    TagModule,
    DialogModule,
    DropdownModule,
    SkeletonModule,
    FloatLabelModule
  ]
})
export class OfficerConfigModule { }
