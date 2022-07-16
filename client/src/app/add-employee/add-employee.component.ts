import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EmployeeNew } from 'src/DTOs/EmployeeNew';
import { Employee } from 'src/models/employee';
import { positionType } from 'src/models/positiontype';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {
  public employee:EmployeeNew={
    id: "00000000-0000-0000-0000-000000000000",
    fullName: "",
    userName: "",
    email: "",
    job: "",
    positionTypeId: "",
    positionType: "",
    isAdmin: false,
    password:""
    };
    positionTypes:positionType[]=[]
  constructor(private employeeService:EmployeeService,private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.employeeService.getAllTypes().subscribe((res:positionType[])=>{
      this.positionTypes=res;
    })
  }

  onSave(){
    this.employeeService.CreateEmployee(this.employee).subscribe(res=>{
      if(res){
        this._snackBar.open('ADDED ', 'Close', {
          duration: 2000,
          panelClass:["backgroundSnackbar"]
        });

      }
    })
  }

}
