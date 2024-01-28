import { Component, HostListener } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})



export class NavbarComponent {
  isScrolled: boolean = false;

  @HostListener('window:scroll', ['$event'])
  onWindowsScroll(event: Event) {
    this.isScrolled = window.pageYOffset === 0 ? false : true;
  }

}
