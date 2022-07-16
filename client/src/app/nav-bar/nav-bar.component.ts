import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from 'src/models/user';
import { AccountFlatService } from '../account/account--flat.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  currentUser$:Observable<IUser>|undefined;
  constructor(private accountService:AccountFlatService) { }

  ngOnInit(): void {
    this.currentUser$=this.accountService.currentUser$;

    console.log(  this.currentUser$)
  }

}
