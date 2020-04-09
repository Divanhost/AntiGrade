import { Criteria } from './criteria.model';

export class Work {
    id: number;
    name = '';
    points: number;
    workTypeId: number;
    criterias: Criteria[];
    subjectId: number;
}
