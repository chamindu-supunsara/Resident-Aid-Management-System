import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { MessageService } from 'primeng/api';
import { LeadsService } from '../../services/common.service';
import { Lead, ProductList, ViewLeadData, ViewLeadSubmitterData, ViewLeadsFilter } from '../../Datamodels/datarequest';
import { NgxSpinnerService } from 'ngx-spinner';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {

  data: any;
  options: any;
  MonthlyRateData: any;
  MonthlyOptions: any;

  Data_1: number[] = [];
  Data_2: number[] = [];
  Data_3: number[] = [];
  Data_4: number[] = [];
  labels: string[] = [];

  ViewLeads: ViewLeadsFilter[] = [];
  LeadsList: Lead[] = [];
  PrductList: ProductList[] = [];
  ViewLeadsData: ViewLeadData = new ViewLeadData();
  ViewLeadsSubmitterData: ViewLeadSubmitterData = new ViewLeadSubmitterData();
  UserId: string = '';
  UserRole: string = '';
  LeadRefNo: string = '';

  LPID: string = '';
  LeadId: string = '';
  Family: number = 0;
  Population: number = 0;
  Aids: number = 0;

  visible: boolean = false;
  metaKey: boolean = true;

  constructor(
    private messageService: MessageService,
    private leadsService: LeadsService,
    private spinner: NgxSpinnerService,
    private cookieService: CookieService
  ) {
    Chart.register(ChartDataLabels);
   }

  show() {
    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Message Content' });
  }

  async ngOnInit() {
    this.spinner.show();
    await this.StatusChart();
    await this.MonthStatusChart();
    await this.GetDashboardStatus();
    this.spinner.hide();
  }

  async GetDashboardStatus() {
    this.UserId = this.cookieService.get('e_userid');
    this.UserRole = this.cookieService.get('e_role');
    const reqParams = { UserId: this.UserId, UserRole: this.UserRole };

    this.leadsService.GetDashboardStatus(reqParams).subscribe(resp => {
      if (resp && resp.length > 0) {
        this.Data_3 = resp[0].data;
        this.Family = this.Data_3[0];
        this.Population = this.Data_3[1];
        this.Aids = this.Data_3[2];
      } else {
        this.Family = 0;
        this.Population = 0;
        this.Aids = 0;
      }
    });
  }

  async MonthStatusChart() {
    this.UserId = this.cookieService.get('e_userid');
    this.UserRole = this.cookieService.get('e_role');
    const reqParams = { UserId: this.UserId, UserRole: this.UserRole };

    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
    const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

    this.leadsService.GetDashboardBar(reqParams).subscribe(resp => {
        if (resp && resp.length > 0) {
            this.Data_1 = resp[0].data;
            this.labels = resp[0].labels;

            this.MonthlyRateData = {
                labels: resp[0].labels,
                datasets: [
                    {
                      type: 'bar',
                      label: ['Population'],
                      backgroundColor: ['#1F1F1F'],
                      data: resp[0].data,
                      borderRadius: 5,
                    }
                ]
            };
        
            this.MonthlyOptions = {
                maintainAspectRatio: false,
                responsive: true,
                aspectRatio: 1,
                plugins: {
                    tooltip: {
                        mode: 'index',
                        intersect: false
                    },
                    legend: {
                      display: false,
                      labels: {
                        color: textColor,
                      }
                    },  
                    datalabels: {
                        display: false,
                        color: "#fff",
                        formatter: function(value: number, context: any) {
                            if (value === 0) {
                                return null;
                            }
                            return value;
                        }
                    }                    
                },
                scales: {
                    x: {
                        stacked: true,
                        ticks: {
                          color: textColorSecondary
                        },
                        grid: {
                          color: surfaceBorder,
                          drawBorder: false
                        }
                    },
                    y: {
                        stacked: true,
                        ticks: {
                            color: textColorSecondary,
                            stepSize: 1,
                            callback: function (value: number) {
                                if (Number.isInteger(value)) {
                                    return value;
                                }
                                return '';
                            }
                        },
                        grid: {
                          color: surfaceBorder,
                          drawBorder: false
                        }
                    }
                }
            };
        } else {
          this.Data_1 = [];
          this.labels = [];
        }
    });
  }

  async StatusChart() {
    this.UserId = this.cookieService.get('e_userid');
    this.UserRole = this.cookieService.get('e_role');
    const reqParams = { UserId: this.UserId, UserRole: this.UserRole };

    this.leadsService.GetDashboardPie(reqParams).subscribe(resp => {
      if (resp && resp[0] && resp[0].data) {
        this.Data_4 = resp[0].data;

        const backgroundColors = ['#000000', '#121212', '#1F1F1F', '#2C2C2C', '#393939', '#464646', '#535353', '#606060', '#6D6D6D', '#7A7A7A', '#878787', '#949494'];
        const hoverColors = ['#000000', '#121212', '#1F1F1F', '#2C2C2C', '#393939', '#464646', '#535353', '#606060', '#6D6D6D', '#7A7A7A', '#878787', '#949494'];

        this.data = {
            labels: resp[0].labels,
            datasets: [
                {
                  data: resp[0].data,
                  backgroundColor: backgroundColors,
                  hoverBackgroundColor: hoverColors
                }
            ]
        };

        this.options = {
          cutout: '60%',
          responsive: true,
          maintainAspectRatio: false,
          plugins: {
            legend: {
              display: false,
              position: 'bottom'
            },
            datalabels: {
              color: "#fff",
                formatter: function(value: number, context: any) {
                  if (value === 0) {
                    return null;
                  }
                    return value;
                }
              } 
            }
        };

      } else {
        this.Data_4 = [];
        this.data = {
          labels: [],
          datasets: []
        };
      }
    });
  }

  getSeverity(status: string): "success" | "secondary" | "info" | "warning" | "danger" | "contrast" {
    switch (status) {
        case 'Accept':
            return 'success';
        case 'Reject':
            return 'warning';
        case 'In Progress':
            return 'info';
        default:
            return 'danger';
    }
  }
}
