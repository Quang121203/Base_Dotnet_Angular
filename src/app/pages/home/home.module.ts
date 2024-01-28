import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { ListItemComponent } from '../../components/list-item/list-item.component';
import { ListComponent } from '../../components/list/list.component';
import { FeaturedComponent } from '../../components/featured/featured.component';
import { HomeComponent } from './home.component';
import { MatIconModule } from '@angular/material/icon';
import{HomeRoutingModule} from './home-routing.module'

@NgModule({
  declarations: [
    NavbarComponent,
    ListItemComponent,
    ListComponent,
    FeaturedComponent,
    HomeComponent
  ],
  imports: [
    MatIconModule,
    CommonModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
