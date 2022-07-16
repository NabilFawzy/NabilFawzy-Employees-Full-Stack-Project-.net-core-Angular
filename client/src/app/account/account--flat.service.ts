import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IUser } from 'src/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountFlatService {

  baseUrl=environment.apiUrl;
  user:IUser={email:'',userName:'',token:''};

  private currentUserSource=new BehaviorSubject<IUser>(this.user);

  currentUser$=this.currentUserSource.asObservable();
  constructor(private http:HttpClient,private router:Router) {
   }

   login(values:any){
    return this.http.post(this.baseUrl + 'account/login',values ).pipe(
      map((user:any)=>{
           if(user){
             localStorage.setItem('token',user.token);
             this.currentUserSource.next(user);
           }
      })
    )
   }


   register(values:any){
    return this.http.post(this.baseUrl + 'account/register',values ).pipe(
      map((user:any)=>{
           if(user){
             localStorage.setItem('token',user.token);
           }
      })
    )
   }

   logout(){
     localStorage.removeItem('token');

     this.currentUserSource.next(this.user);
     this.router.navigateByUrl('/')
   }




}
