import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NgxSpinnerModule } from 'ngx-spinner';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { TagModule } from 'primeng/tag';
import { MessageService } from 'primeng/api';
import { DialogModule } from 'primeng/dialog';
import { FloatLabelModule } from 'primeng/floatlabel';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { SkeletonModule } from 'primeng/skeleton';
import { CalendarModule } from 'primeng/calendar';
import { CheckboxModule } from 'primeng/checkbox';
import { InputMaskModule } from 'primeng/inputmask';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { MultiSelectModule } from 'primeng/multiselect';
import { MembersRoutingModule } from './members-routing.module';
import { MembersComponent } from './members.component';

@NgModule({
  declarations: [MembersComponent],
  providers: [MessageService],
  imports: [
    CommonModule,
    MembersRoutingModule,
    NgxSpinnerModule,
    CardModule,
    TableModule,
    ToastModule,
    TagModule,
    DialogModule,
    FloatLabelModule,
    InputTextareaModule,
    IconFieldModule,
    InputIconModule,
    MultiSelectModule,
    ButtonModule,
    DropdownModule,
    FormsModule,
    InputTextModule,
    ReactiveFormsModule,
    SkeletonModule,
    CalendarModule,
    InputMaskModule,
    InputNumberModule,
    CheckboxModule,
  ],
})
export class MembersModule {}
