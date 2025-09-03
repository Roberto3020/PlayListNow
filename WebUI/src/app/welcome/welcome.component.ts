import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css'],
  standalone: true,
  imports: [CommonModule, MatCardModule, MatToolbarModule, MatButtonModule, MatIconModule]
})
export class WelcomeComponent {
  constructor(private router: Router) {}

  goToCreate() {
    this.router.navigate(['/songs/add']);
  }

  goToList() {
    this.router.navigate(['/songs']);
  }
}
