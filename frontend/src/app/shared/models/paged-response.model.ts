export class PagedResponseModel<T> {
    payload: T[];
    summary: AccountReportSummary;
}

export class SummaryBase {
    pageNumber = 1;
    pageSize = 10;
    totalItems = 0;

    constructor(pageSize = 10) {
        this.pageSize = pageSize;
    }
}

export class AccountReportSummary extends SummaryBase {
    balance = 0;
    totalIncome = 0;
    totalExpense = 0;
}
