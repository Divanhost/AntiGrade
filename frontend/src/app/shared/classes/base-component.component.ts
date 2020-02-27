import { OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

export class BaseComponent implements OnDestroy {
    public subscriptions: Subscription[] = [];

    ngOnDestroy() {
        this.subscriptions.forEach(x => x.unsubscribe());
    }
}
