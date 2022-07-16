import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Employee } from 'src/models/employee';
import { positionType } from 'src/models/positiontype';
import { EmployeeService } from './employee.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'client';

  employees:Employee[]=[];
  positionTypes:positionType[]=[]
  constructor(private _snackBar: MatSnackBar, private employeeService:EmployeeService ){

  }
  ngOnInit(): void {

    this.employeeService.getAllTypes().subscribe((res:positionType[])=>{
      this.positionTypes=res;
    })





    this.getAllEmp();

  }

  getAllEmp(){
    this.employeeService.getAllEmployees().subscribe((res:Employee[])=>{
      this.employees=res;
   });
  }

  updateEmp(emp:Employee){
     emp.positionType=this.positionTypes.filter(x=>x.id==emp.positionTypeId)[0].name;
  }


  onEdit(emp:Employee){
    console.log(emp)
    this.employeeService.UpdateEmployee(emp).subscribe(res=>{
      if(res){
        this._snackBar.open('Updated ', 'Close', {
          duration: 2000,
          panelClass:["backgroundSnackbar"]
        });
        this.getAllEmp();
      }
    })

  }
  onDelete(emp2:Employee){
    this.employeeService.DeleteEmployee(emp2).subscribe(res=>{
      if(res){

    this._snackBar.open('Deleted ', 'Close', {
      duration: 2000,
      panelClass:["backgroundRedSnackbar"]
    });
        this.getAllEmp();
      }
    })



  }


}
