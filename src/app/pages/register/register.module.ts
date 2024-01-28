import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { RegisterRoutingModule } from './register-routing.module'
import { RegisterComponent } from './register.component';
import {FormsModule} from '@angular/forms';

@NgModule({
  declarations: [
    RegisterComponent
  ],
  imports: [
    MatIconModule,
    CommonModule,
    RegisterRoutingModule,
    FormsModule
  ]
})
export class RegisterModule { }
