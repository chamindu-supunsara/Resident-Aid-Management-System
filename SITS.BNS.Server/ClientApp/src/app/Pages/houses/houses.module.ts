import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HousesRoutingModule } from './houses-routing.module';
import { HousesComponent } from './houses.component';
import { TableModule } from 'primeng/table';
import { CardModule } from 'primeng/card';
import { NgxSpinnerModule } from 'ngx-spinner';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { SkeletonModule } from 'primeng/skeleton';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { InputMaskModule } from 'primeng/inputmask';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { RippleModule } from 'primeng/ripple';

@NgModule({
  declarations: [HousesComponent],
  providers: [MessageService],
  imports: [
    CommonModule,
    HousesRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    TableModule,
    ButtonModule,
    RippleModule,
    CalendarModule,
    DropdownModule,
    CheckboxModule,
    InputMaskModule,
    InputTextareaModule,
    InputTextModule,
    FloatLabelModule,
    CardModule,
    NgxSpinnerModule,
    ToastModule,
    TagModule,
    DialogModule,
    SkeletonModule,
  ],
})
export class HousesModule {}
