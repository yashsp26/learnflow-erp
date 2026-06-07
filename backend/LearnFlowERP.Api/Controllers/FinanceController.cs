using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.Finance.Queries.GetCollectionSummary;
using LearnFlowERP.Application.Features.Finance.Queries.GetFeeDefaulters;
using LearnFlowERP.Application.Features.Finance.Queries.GetFinanceDashboard;
using LearnFlowERP.Application.Features.Finance.Queries.GetMonthlyCollection;
using LearnFlowERP.Application.Features.Finance.Queries.GetOutstandingFees;
using LearnFlowERP.Application.Features.Finance.Queries.GetStudentLedger;
using LearnFlowERP.Application.Features.Finance.Queries.GetStudentStatement;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/finance")]
    public class FinanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Permission("ViewFinanceDashboard")]
        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            return Ok(
                await _mediator.Send(
                    new GetFinanceDashboardQuery()));
        }

        [Permission("ViewFinanceDashboard")]
        [HttpGet("outstanding")]
        public async Task<IActionResult> Outstanding()
        {
            return Ok(
                await _mediator.Send(
                    new GetOutstandingFeesQuery()));
        }

        [Permission("ViewFinanceDashboard")]
        [HttpGet("monthly-collection")]
        public async Task<IActionResult> MonthlyCollection()
        {
            return Ok(
                await _mediator.Send(
                    new GetMonthlyCollectionQuery()));
        }

        [Permission("ViewFinanceReports")]
        [HttpGet("summary")]
        public async Task<IActionResult> Summary()
        {
            return Ok(
                await _mediator.Send(
                    new GetCollectionSummaryQuery()));
        }

        [Permission("ViewFinanceReports")]
        [HttpGet("defaulters")]
        public async Task<IActionResult> Defaulters()
        {
            return Ok(
                await _mediator.Send(
                    new GetFeeDefaulterQuery()));
        }

        [Permission("ViewFinanceStatement")]
        [HttpGet("student/{studentId}/statement")]
        public async Task<IActionResult> StudentStatement(
            long studentId)
        {
            return Ok(
                await _mediator.Send(
                    new GetStudentStatementQuery(studentId)));
        }

        [Permission("ViewFinanceStatement")]
        [HttpGet("student/{studentId}/ledger")]
        public async Task<IActionResult> Ledger(long studentId)
        {
            return Ok(
                await _mediator.Send(
                    new GetStudentLedgerQuery(studentId)));
        }
    }
}