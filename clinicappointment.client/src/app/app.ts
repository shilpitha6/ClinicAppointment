import { HttpClient } from '@angular/common/http';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { RouterLink } from '@angular/router';

import { CommonModule } from '@angular/common';
import { AuthService } from './Services/AuthService';


@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: true,
  styleUrl: './app.css',
  imports: [RouterOutlet, RouterLink, CommonModule]
})
export class App   {

  constructor(public authService: AuthService) { }


}
