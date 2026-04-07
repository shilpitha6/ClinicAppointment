import { Component, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../Services/AuthService';
import { FormsModule } from '@angular/forms';
import { PatientDashboardService } from '../../Services/PatientDashboardService';


@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.html',
  styleUrl: './login.css',
  imports: [FormsModule, RouterLink]
})
export class Login {
  form = {
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

    this.patientService.login(this.form).subscribe({
      next: (patient) => {
        this.authService.setPatient(patient);
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.serverError.set(err.error?.message ?? 'Login failed. Please try again.');
        this.loading.set(false);
      }
    });
  }
}






