import { Component, OnInit, signal } from '@angular/core';
import { BranchLocation, Companies } from '../../Datamodels/datarequest';
import { CookieService } from 'ngx-cookie-service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-branch-map',
  templateUrl: './branch-map.component.html',
  styleUrl: './branch-map.component.css'
})
export class BranchMapComponent implements OnInit {

  center = signal<google.maps.LatLngLiteral>({lat: 7.8731, lng: 80.7718});
  zoom = signal(8);
  UserRole: string = '';
  UserId: string = '';
  CompanyID: number = 0;
  locations: BranchLocation [] = [];
  Companies: Companies[] = [];
  selectCompany: Companies | undefined;
  OnSelectLocation: BranchLocation | undefined;

  async ngOnInit(): Promise<void> {
    this.spinner.show();
    this.getLocations();
    this.spinner.hide();
  }

  constructor(
    private cookieService: CookieService,
    private spinner: NgxSpinnerService
  ) { }

  getLocations() {
    this.UserRole = this.cookieService.get('e_role');
    this.UserId = this.cookieService.get('e_userid');

    if (this.UserRole === 'LGS Admin') {

    } else {
      const reqParams = { UserId: this.UserId };
    }
  }

  SelectCompany() {
    
  }

  onReorder(event: any) {
    this.spinner.show();
    setTimeout(() => {
      this.OnSelectLocation = event.value[0];
      if (this.OnSelectLocation) {
        this.center.set({ lat: this.OnSelectLocation.latitude, lng: this.OnSelectLocation.longitude });
      }
      this.zoom.set(15);
      this.spinner.hide();
    }, 500);
  }  
}
