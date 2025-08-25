import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { ProcessInfo } from '../../models/ProcessInfo';
import { AsyncPipe, DatePipe, KeyValuePipe } from '@angular/common';
import { EnvironmentService } from '../../services/environment.service';
import { Observable } from 'rxjs';
import { LengthPipe } from '../../pipes/lenght.pipe';

@Component({
  selector: 'app-process-details-modal',
  standalone: true,
  imports: [MatDialogModule, DatePipe, KeyValuePipe, AsyncPipe, LengthPipe],
  templateUrl: './process-details-modal.component.html',
  styleUrl: './process-details-modal.component.css'
})
export class ProcessDetailsModalComponent {
  envVars$!: Observable<Record<string, string>>;

  constructor(
    public dialogRef: MatDialogRef<ProcessDetailsModalComponent>,
    @Inject(MAT_DIALOG_DATA) public process: ProcessInfo,
    private envService: EnvironmentService
  ) {
    this.envVars$ = this.envService.getEnvironmentVariables();
  }

  close(): void {
    this.dialogRef.close();
  }
}
