import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { ConfirmDialogComponent } from 'src/app/components/core/dialogs/confirm-dialog/confirm-dialog.component';
import { ConfirmDialogData } from 'src/app/models/core/confirm-dialog-data';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private _dialog: MatDialog) { }

  confirmDialog(data: ConfirmDialogData): Observable<boolean> {
    return this._dialog.open(ConfirmDialogComponent, {
      data,
      width: '400px',
    }).afterClosed();
  }

}
