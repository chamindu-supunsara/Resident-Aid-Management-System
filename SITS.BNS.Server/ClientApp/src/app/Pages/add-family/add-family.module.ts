import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AddFamilyRoutingModule } from './add-family-routing.module';
import { AddFamilyComponent } from './add-family.component';
import { CardModule } from 'primeng/card';
import { StepperModule } from 'primeng/stepper';
import { ButtonModule } from 'primeng/button';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { ListboxModule } from 'primeng/listbox';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MultiSelectModule } from 'primeng/multiselect';
import { TabViewModule } from 'primeng/tabview';
import { InputMaskModule } from 'primeng/inputmask';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { NgxSpinnerModule } from 'ngx-spinner';
import { InputNumberModule } from 'primeng/inputnumber';
import { SkeletonModule } from 'primeng/skeleton';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';

@NgModule({
  declarations: [AddFamilyComponent],
  providers: [MessageService],
  imports: [
    CommonModule,
    AddFamilyRoutingModule,
    ButtonModule,
    DropdownModule,
    StepperModule,
    CardModule,
    CalendarModule,
    FloatLabelModule,
    InputTextModule,
    ListboxModule,
    FormsModule,
    MultiSelectModule,
    TabViewModule,
    InputMaskModule,
    ReactiveFormsModule,
    ToastModule,
    NgxSpinnerModule,
    InputNumberModule,
    CheckboxModule,
    InputTextareaModule,
    SkeletonModule,
  ],
})
export class AddFamilyModule {}
