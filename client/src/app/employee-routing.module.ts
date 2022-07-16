import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ListEmployeesComponent } from './list-employees/list-employees.component';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { AddEmployeeComponent } from './add-employee/add-employee.component';



@NgModule({
  declarations: [NavBarComponent,ListEmployeesComponent,AddEmployeeComponent],
  imports: [
    CommonModule,   FormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatSnackBarModule,

  ],
  exports:[
    NavBarComponent,ListEmployeesComponent,AddEmployeeComponent

  ]
})
export class EmployeeRoutingModule { }
