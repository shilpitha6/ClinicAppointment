import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../Services/AuthService';
import { PatientDashboardService } from '../../Services/PatientDashboardService';

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
  errors: Partial<typeof this.form> = {};
  serverError = signal('');
  loading = signal(false);

  constructor(
    private patientService: PatientDashboardService,
    private authService: AuthService,
    private router: Router
  ) { }

  validate(): boolean {
    this.errors = {};

    if (!this.form.first_name.trim())
      this.errors.first_name = 'First name is required.';

    if (!this.form.last_name.trim())
      this.errors.last_name = 'Last name is required.';

    if (!this.form.dob)
      this.errors.dob = 'Date of birth is required.';

    if (!this.form.gender)
      this.errors.gender = 'Gender is required.';

    if (!this.form.email)
      this.errors.email = 'Email is required.';
    

    if (!this.form.username)
      this.errors.username = 'Username is required.';
    

    if (!this.form.password)
      this.errors.password = 'Password is required.';
    
    return Object.keys(this.errors).length === 0;
  }

  submit(): void {
    this.serverError.set('');
    if (!this.validate()) return;

    this.loading.set(true);

    this.patientService.register(this.form).subscribe({
      next: (patient) => {
        this.authService.setPatient(patient);
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.serverError.set(err.error?.message ?? 'Registration failed. Please try again.');
        this.loading.set(false);
      }
    });
  }
}
