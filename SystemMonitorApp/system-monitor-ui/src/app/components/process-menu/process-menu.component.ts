import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { ProcessInfo } from '../../models/ProcessInfo';
import { ProcessService } from '../../services/process.service';
import { MatDialog } from '@angular/material/dialog';
import { ProcessDetailsModalComponent } from '../process-details-modal/process-details-modal.component';

@Component({
  selector: 'app-process-menu',
  standalone: true,
  imports: [MatIconModule, MatMenuModule, MatButtonModule],
  templateUrl: './process-menu.component.html',
  styleUrl: './process-menu.component.css'
})
export class ProcessMenuComponent {
  @Input() process!: ProcessInfo;
  @Output() refreshProcesses = new EventEmitter<void>();

  constructor(private readonly service: ProcessService,
    private dialog: MatDialog
  ) { }

  increasePriority() {
    this.service.increasePriority(this.process.id).subscribe({
      next: () => {
        console.log(`Process ${this.process.name} requested higher priority`);
        this.refreshProcesses.emit();
      }
    });
  }

  decreasePriority() {
    this.service.decreasePriority(this.process.id).subscribe({
      next: () => {
        console.log(`Process ${this.process.name} requested lower priority`);
        this.refreshProcesses.emit();
      }
    });
  }

  showDetails() {
    this.dialog.open(ProcessDetailsModalComponent, {
      minWidth: '1000px',
      minHeight: '600px',
      data: this.process
    })
  }

  killProcess() {
    this.service.killProcess(this.process.id).subscribe({
      next: () => {
        console.log(`Process ${this.process.name} is killed successfully`);
        this.refreshProcesses.emit();
      },
    });
  }
}
