import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { ProcessInfo } from '../../models/ProcessInfo';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-process-details-modal',
  standalone: true,
  imports: [MatDialogModule, DatePipe],
  templateUrl: './process-details-modal.component.html',
  styleUrl: './process-details-modal.component.css'
})
export class ProcessDetailsModalComponent {
  constructor(
    public dialogRef: MatDialogRef<ProcessDetailsModalComponent>,
    @Inject(MAT_DIALOG_DATA) public process: ProcessInfo
  ) { }


  
  close(): void {
    this.dialogRef.close();
  }
}
