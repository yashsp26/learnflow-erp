import api from "./axios";
import type { PaymentMethod } from "../types/payment";

export const createPaymentApi = async (feeId: number, amountPaid: number, paymentMethod: PaymentMethod, transactionRef?: string, remarks?: string) => {
  const response = await api.post("/payments", { feeId, amountPaid, paymentMethod, transactionRef: transactionRef || undefined, remarks: remarks || undefined });
  return response.data;
};
export const getPaymentByIdApi = async (paymentId: number) => (await api.get(`/payments/${paymentId}`)).data;
export const getStudentPaymentsApi = async (studentId: number) => (await api.get(`/payments/student/${studentId}`)).data;
export const getReceiptApi = async (paymentId: number) => (await api.get(`/payments/${paymentId}/receipt`)).data;
export const downloadReceiptPdfApi = async (paymentId: number) => (await api.get(`/payments/${paymentId}/receipt/pdf`, { responseType: "blob" })).data as Blob;
