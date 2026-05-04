import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuditLogRoutingModule } from './audit-log-routing.module';
import { AuditLogComponent } from './audit-log.component';
import { MessageService } from 'primeng/api';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastModule } from 'primeng/toast';
import { SkeletonModule } from 'primeng/skeleton';


@NgModule({
  declarations: [AuditLogComponent],
  providers: [MessageService],
  imports: [
    CommonModule,
    AuditLogRoutingModule,
    TableModule,
    CardModule,
    NgxSpinnerModule,
    ToastModule,
    TagModule,
    SkeletonModule
  ]
})
export class AuditLogModule { }
