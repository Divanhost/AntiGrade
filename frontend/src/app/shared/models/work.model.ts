import { Criteria } from './criteria.model';

export class Work {
    id: number;
    name: string;
    points: number;
    criterias: Criteria[];
}
