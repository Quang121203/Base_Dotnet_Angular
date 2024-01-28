import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { WatchRoutingModule } from './watch-routing.module'
import { WatchComponent } from './watch.component';

@NgModule({
  declarations: [
    WatchComponent
  ],
  imports: [
    MatIconModule,
    CommonModule,
    WatchRoutingModule
  ]
})
export class WatchModule { }
