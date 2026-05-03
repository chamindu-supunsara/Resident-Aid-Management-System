import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BranchMapComponent } from './branch-map.component';

const routes: Routes = [{ path: '', component: BranchMapComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BranchMapRoutingModule { }
