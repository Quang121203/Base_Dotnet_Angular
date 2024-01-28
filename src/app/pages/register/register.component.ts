import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  email: string = '';
  inputEmail: string = '';

  handleClick = () => {
    this.email = this.inputEmail;
  }
}
