using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lecture_BlazorRankingApplication.Data.Models
{
    // v3.0 GameResults 없음.
    // v4.0 GameReuslts 등장.
    // 모종의 이유로 v4.0 -> v3.0으로 가게 될 경우,
    // GameResults Table을 Down 시킬필요가 있다.
    // 그렇기 때문에, Migrations의 RankingService에 Down 함수도 있는 것.

    public class GameResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
    }
}
