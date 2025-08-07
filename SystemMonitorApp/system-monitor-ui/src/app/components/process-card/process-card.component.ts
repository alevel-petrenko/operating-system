import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { ProcessInfo } from '../../models/ProcessInfo';
import { ProcessMenuComponent } from '../process-menu/process-menu.component';
import { ProcessPriority } from '../../models/ProcessPriority';
import { LengthPipe } from '../../pipes/lenght.pipe';

@Component({
  selector: 'app-process-card',
  standalone: true,
  imports: [MatCardModule, MatChipsModule, ProcessMenuComponent, LengthPipe ],
  templateUrl: './process-card.component.html',
  styleUrl: './process-card.component.css'
})
export class ProcessCardComponent {
  @Input() process!: ProcessInfo;
  @Output() refreshProcesses = new EventEmitter<void>();
  public priorityEnum = ProcessPriority;

  public onRefreshProcesses: () => void = () => {
    console.log(`Refreshing processes for ${this.process.name}`);
    this.refreshProcesses.emit();
  };
}
