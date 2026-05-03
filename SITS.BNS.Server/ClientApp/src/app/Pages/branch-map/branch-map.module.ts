import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BranchMapRoutingModule } from './branch-map-routing.module';
import { GoogleMap, MapAdvancedMarker } from '@angular/google-maps';
import { BranchMapComponent } from './branch-map.component';
import { CardModule } from 'primeng/card';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { DropdownModule } from 'primeng/dropdown';
import { FloatLabelModule } from 'primeng/floatlabel';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FormsModule } from '@angular/forms';
import { OrderListModule } from 'primeng/orderlist';


@NgModule({
  declarations: [BranchMapComponent],
  providers: [MessageService],
  imports: [
    CommonModule,
    BranchMapRoutingModule,
    GoogleMap,
    CardModule,
    MapAdvancedMarker,
    ToastModule,
    DropdownModule,
    FloatLabelModule,
    NgxSpinnerModule,
    FormsModule,
    OrderListModule
  ]
})
export class BranchMapModule { }
