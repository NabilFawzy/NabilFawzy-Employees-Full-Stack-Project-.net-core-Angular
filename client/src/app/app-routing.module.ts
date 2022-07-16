import {  NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { AppComponent } from './app.component';
import { ListEmployeesComponent } from './list-employees/list-employees.component';

const routes: Routes = [
  {path:'',component:ListEmployeesComponent},
  {path:'add-employee',component:AddEmployeeComponent},
   {path:'account',loadChildren:()=>import('./account/account.module')
   .then(mod=>mod.AccountModule),data:{breadcrumb:{skip:true}}}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
