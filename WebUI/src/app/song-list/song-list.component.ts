import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { SongService, Song } from '../Service/song.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-song-list',
  templateUrl: './song-list.component.html',
  styleUrls: ['./song-list.component.css'],
  standalone: true,
  imports: [CommonModule, MatCardModule, MatToolbarModule, MatButtonModule, MatIconModule, MatTableModule]
})
export class SongListComponent {
  songs: Song[] = [];
  loading = true;

  constructor(
    private songService: SongService,
    private router: Router,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) {
    this.loadSongs();
  }

  loadSongs() {
    this.songService.getSongs().subscribe({
      next: (response: any) => {
        if (response.success) {
          this.songs = response.data || [];
        } else {
          const errorMsg = response.errors?.[0] || response.message || 'Error al consultar las canciones';
          this.snackBar.open(errorMsg, 'Cerrar', {
            duration: 3000,
            panelClass: ['snackbar-error']
          });
        }
        this.loading = false;
      },
      error: (error) => {
        const errorMsg = error?.error?.errors?.[0] || error?.error?.message || 'Error al consultar las canciones';
        this.snackBar.open(errorMsg, 'Cerrar', {
          duration: 3000,
          panelClass: ['snackbar-error']
        });
        this.loading = false;
      }
    });
  }

  editSong(song: Song) {
    this.router.navigate(['/songs/edit', song.id]);
  }

  deleteSong(song: Song) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { title: song.title }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (song.id !== undefined) {
          this.songService.deleteSong(song.id).subscribe({
            next: (response: any) => {
              if (response.success) {
                this.snackBar.open(response.message || 'Canción eliminada exitosamente!', 'Cerrar', {
                  duration: 3000,
                  panelClass: ['snackbar-success']
                });
                this.loadSongs();
              } else {
                const errorMsg = response.errors?.[0] || response.message || 'Error al eliminar la canción';
                this.snackBar.open(errorMsg, 'Cerrar', {
                  duration: 3000,
                  panelClass: ['snackbar-error']
                });
              }
            },
            error: (error) => {
              const errorMsg = error?.error?.errors?.[0] || error?.error?.message || 'Error al eliminar la canción';
              this.snackBar.open(errorMsg, 'Cerrar', {
                duration: 3000,
                panelClass: ['snackbar-error']
              });
            }
          });
        } else {
          this.snackBar.open('No se puede eliminar la canción porque el ID no está definido.', 'Cerrar', {
            duration: 3000,
            panelClass: ['snackbar-error']
          });
        }
      }
    });
  }

  goToWelcome() {
    this.router.navigate(['/']);
  }

  addSong() {
    this.router.navigate(['/songs/add']);
  }
}
