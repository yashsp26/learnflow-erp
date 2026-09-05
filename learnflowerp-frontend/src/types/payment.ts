export const paymentMethods = ["Cash", "UPI", "Card", "BankTransfer", "Cheque"] as const;
export const paymentStatuses = ["Pending", "Success", "Failed", "Refunded", "Cancelled"] as const;
export type PaymentMethod = (typeof paymentMethods)[number];
export type PaymentStatus = (typeof paymentStatuses)[number];

export interface Payment {
  paymentId: number;
  feeId: number;
  amountPaid: number;
  paymentMethod: PaymentMethod;
  status: PaymentStatus;
  receiptNumber: string;
  transactionRef: string | null;
  paymentDate: string;
}

export interface Receipt {
  receiptNumber: string;
  paymentId: number;
  studentName: string;
  feeType: string;
  amountPaid: number;
  paymentMethod: string;
  transactionRef: string | null;
  paymentDate: string;
}
