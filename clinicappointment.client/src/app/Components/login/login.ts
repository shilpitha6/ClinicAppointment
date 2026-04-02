//import { Component, signal } from '@angular/core';
//import { Router, RouterLink } from '@angular/router';
//import { AuthService } from '../../Services/AuthService';
//import { FormsModule } from '@angular/forms';


//@Component({
//  selector: 'app-login',
//  standalone: true,
//  templateUrl: './login.html',
//  styleUrl: './login.css',
//  imports: [FormsModule, RouterLink]
//})
//export class Login {
//  username = signal<string>('');
//  password = signal<string>('');
//  loading = signal(false);
//  error = signal('');

//  constructor(
//    private authService: AuthService,
//    private router: Router) {
   
//  }

//onSubmit(): void {
//  this.loading.set(true);
//  this.error.set('');

//  this.authService.login({
//    username: this.username(),
//    password: this.password()
//  }).subscribe({
//    next: () => this.router.navigate(['/doctors']),
//    error: err => {
//      this.error.set(err.error?.message ?? 'invalid username or password');
//      this.loading.set(false);

//    }
//  })
//}





//}
