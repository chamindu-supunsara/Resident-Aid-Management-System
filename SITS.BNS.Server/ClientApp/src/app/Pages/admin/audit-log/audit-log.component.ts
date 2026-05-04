import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageService } from 'primeng/api';
import { AuditsLogs } from '../../../Datamodels/datarequest';
import { LeadsService } from '../../../services/common.service';

@Component({
  selector: 'app-audit-log',
  templateUrl: './audit-log.component.html',
  styleUrl: './audit-log.component.css'
})
export class AuditLogComponent {

  AuditsLogs: AuditsLogs[] = [];
  metaKey: boolean = false;

  constructor(
    private messageService: MessageService,
    private leadsService: LeadsService,
    private spinner: NgxSpinnerService
  ) {  }

  async ngOnInit() {
    this.spinner.show();
    await this.AllAuditLogs();
    this.spinner.hide();
  }

  async AllAuditLogs() {
    this.leadsService.GetAllAuditLogs().subscribe(resp => {
      if (resp) {
        this.AuditsLogs = resp;
      } else {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Data Load Failed' });
      }
    });
  }

}
