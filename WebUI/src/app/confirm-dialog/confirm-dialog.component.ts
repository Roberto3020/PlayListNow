
import { Component } from '@angular/core';
import { Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-confirm-dialog',
  template: `
    <div style="display: flex; align-items: center; gap: 12px;">
      <mat-icon color="warn" style="font-size: 32px;">delete_forever</mat-icon>
      <h2 mat-dialog-title style="margin: 0; font-weight: 600; color: #d32f2f;">¿Confirmar eliminación?</h2>
    </div>
    <mat-dialog-content>
      <p style="font-size: 1.1em; color: #444; margin-top: 8px;">¿Seguro que quieres eliminar la canción <b>{{data.title}}</b>? Esta acción no se puede deshacer.</p>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-stroked-button color="primary" mat-dialog-close="false" style="border-radius: 8px; margin-right: 8px;">Cancelar</button>
      <button mat-raised-button color="warn" mat-dialog-close="true" style="border-radius: 8px;">Eliminar</button>
    </mat-dialog-actions>
  `,
  styleUrls: ['./confirm-dialog.component.css'],
  standalone: true,
  imports: [MatDialogModule, MatIconModule,MatButtonModule]
})
export class ConfirmDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: { title: string }) {}
}
