import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddFamilyComponent } from './add-family.component';

const routes: Routes = [{ path: '', component: AddFamilyComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AddFamilyRoutingModule { }
