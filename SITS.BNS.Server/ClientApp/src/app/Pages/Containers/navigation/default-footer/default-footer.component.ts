import { Component } from '@angular/core';
import { FooterComponent } from '@coreui/angular';

@Component({
  selector: 'app-default-footer',
  templateUrl: './default-footer.component.html',
  styleUrl: './default-footer.component.scss',
  standalone: true,
})
export class DefaultFooterComponent extends FooterComponent{

  year: number;

  constructor() {
    super();
    this.year = new Date().getFullYear();  
  }
}
