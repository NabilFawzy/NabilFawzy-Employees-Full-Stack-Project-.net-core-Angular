import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError } from 'rxjs';
import { AccountFlatService } from '../account--flat.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm:FormGroup=new FormGroup({
    email:new FormControl('',Validators.required),
    password:new FormControl('',Validators.required)
  })
  constructor(private accountFlatService:AccountFlatService) { }

  ngOnInit(): void {
    this.createLoginForm()
  }

  createLoginForm(){
    this.loginForm=new FormGroup({
      email:new FormControl('',Validators.required),
      password:new FormControl('',Validators.required)
    })
  }

  onSubmit(){
    console.log(this.loginForm?.value)

    this.accountFlatService.login(this.loginForm.value).subscribe(()=>
    {
      console.log('user logged in')
    },
    error =>{
      console.log(error)
    }
    )
  }

}
