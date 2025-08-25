import { Pipe, PipeTransform } from "@angular/core";
import { max } from "rxjs";

@Pipe({
    name: 'length',
    standalone: true
})

export class LengthPipe implements PipeTransform {
    transform(value: string, maxLength: number = 40): string {
        if (!value)
            return '';

        return value.length > maxLength
            ? value.substring(0, maxLength) + '...'
            : value;
    }
}