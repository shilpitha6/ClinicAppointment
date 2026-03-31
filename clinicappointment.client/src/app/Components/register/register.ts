import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../Services/AuthService';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  form = {
    first_name: '',
    last_name: '',
    dob: '',
    gender: '',
    email: '',
    address: '',
    username: '',
    password: ''
  };

  loading = signal(false);
  error = signal('');

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  onSubmit(): void {
    this.loading.set(true);
    this.error.set('');

    this.authService.register(this.form).subscribe({
      next: () => this.router.navigate(['/login']),
      error: err => {
        this.error.set(err.error?.message ?? 'Registration failed.');
        this.loading.set(false);
      }
    });
  }
}
