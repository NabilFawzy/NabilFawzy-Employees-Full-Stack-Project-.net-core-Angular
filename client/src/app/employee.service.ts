import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Employee } from 'src/models/employee';
import { positionType } from 'src/models/positiontype';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  baseUrl=environment.apiUrl;
  constructor(private http:HttpClient) {

   }

   getAllEmployees(){
    return this.http.get<Employee[]>(this.baseUrl+'Employees');
  }

  getAllTypes(){
    return this.http.get<positionType[]>(this.baseUrl+'Employees/types');
  }


  UpdateEmployee(emp:Employee){
    return this.http.post<boolean>(this.baseUrl+'Employees/UpdateEmployee',emp);
  }
  CreateEmployee(emp:Employee){
    return this.http.post<boolean>(this.baseUrl+'Employees/CreateEmployee',emp);

  }
  DeleteEmployee(emp:Employee){
    return this.http.get<boolean>(this.baseUrl+'Employees/DeleteEmployee/'+emp.id);
  }

}
