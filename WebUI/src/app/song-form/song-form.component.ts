import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SongService, Song } from '../Service/song.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
  selector: 'app-song-form',
  templateUrl: './song-form.component.html',
  styleUrls: ['./song-form.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule, MatCardModule, MatToolbarModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatIconModule, MatDatepickerModule, MatNativeDateModule]
})
export class SongFormComponent {
  song: Partial<Song> = {};
  isEdit = false;
  loading = false;

  constructor(
    private songService: SongService,
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEdit = true;
      this.loading = true;
      this.songService.getSong(+id).subscribe({
        next: (res: any) => {
          if (res.success) {
            this.song = res.data;
          } else {
            this.snackBar.open(res.message || 'Error al cargar la canción', 'Cerrar', {
              duration: 3000,
              panelClass: ['snackbar-error']
            });
          }
          this.loading = false;
        },
        error: (error) => {
          const errorMsg = error?.error?.errors?.[0] || error?.error?.message || 'Error al cargar la canción';
          this.snackBar.open(errorMsg, 'Cerrar', {
            duration: 3000,
            panelClass: ['snackbar-error']
          });
          this.loading = false;
        }
      });
    }
  }

  saveSong() {
    const payload = {
      title: this.song.title,
      artist: this.song.artist,
      duration: this.song.duration
    };
    if (this.isEdit && this.song.id) {
      this.songService.updateSong({ ...payload, id: this.song.id } as Song).subscribe({
        next: (response: any) => {
          if (response.success) {
            this.snackBar.open(response.message || 'Canción actualizada exitosamente!', 'Cerrar', {
              duration: 3000,
              panelClass: ['snackbar-success']
            });
            this.router.navigate(['/songs']);
          } else {
            const errorMsg = response.errors?.[0] || response.message || 'Error al actualizar la canción';
            this.snackBar.open(errorMsg, 'Cerrar', {
              duration: 3000,
              panelClass: ['snackbar-error']
            });
          }
        },
        error: (error) => {
          const errorMsg = error?.error?.errors?.[0] || error?.error?.message || 'Error al actualizar la canción';
          this.snackBar.open(errorMsg, 'Cerrar', {
            duration: 3000,
            panelClass: ['snackbar-error']
          });
        }
      });
    } else {
      this.songService.addSong(payload as Song).subscribe({
        next: (response: any) => {
          if (response.success) {
            this.snackBar.open(response.message || 'Canción guardada exitosamente!', 'Cerrar', {
              duration: 3000,
              panelClass: ['snackbar-success']
            });
            this.router.navigate(['/songs']);
          } else {
            const errorMsg = response.errors?.[0] || response.message || 'Error al guardar la canción';
            this.snackBar.open(errorMsg, 'Cerrar', {
              duration: 3000,
              panelClass: ['snackbar-error']
            });
          }
        },
        error: (error) => {
          const errorMsg = error?.error?.errors?.[0] || error?.error?.message || 'Error al guardar la canción';
          this.snackBar.open(errorMsg, 'Cerrar', {
            duration: 3000,
            panelClass: ['snackbar-error']
          });
        }
      });
    }
  }

  cancel() {
    this.router.navigate(['/songs']);
  }

  goToWelcome() {
    this.router.navigate(['/']);
  }

}
