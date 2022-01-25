using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MTIPC.MQL;

namespace MTIPC.Communications
{
    public class Request
    {
        private MTOperation mtOp;
        public string symbol;
        public double volume;
        public double sl;
        public double tp;
        public double entry;
        public MTOrderType type;
        public DateTime specified;
        public long ticket;
        int count;
        bool orderbook;
        DateTime start;
        DateTime end;
        TIMEFRAME timeframe;

        #region New Order Request

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="volume"></param>
        /// <param name="sl"></param>
        /// <param name="tp"></param>
        /// <param name="entry"></param>
        /// <param name="type"></param>
        public Request(string symbol, double volume, double sl = 0.0, double tp = 0.0, double entry = 0.0, MTOrderType type = MTOrderType.GTC)
        {
            mtOp = MTOperation.NewOrder;
            this.symbol = symbol;
            this.volume = volume;
            this.sl = sl;
            this.tp = tp;
            this.entry = entry;
            this.type = type;
        }

        /// <summary>
        /// Create a new order with an expiration date
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="volume"></param>
        /// <param name="specified"></param>
        /// <param name="sl"></param>
        /// <param name="tp"></param>
        /// <param name="entry"></param>
        public Request(string symbol, double volume, DateTime specified, double sl = 0.0, double tp = 0.0, double entry = 0.0)
        {
            mtOp = MTOperation.NewOrder;
            this.symbol = symbol;
            this.volume = volume;
            this.specified = specified;
            this.sl = sl;
            this.tp = tp;
            this.entry = entry;
            this.type = MTOrderType.Specified;
        }

        #endregion

        #region Modify Order Request

        /// <summary>
        /// Modify Take Profit and Stop Loss of an existing order or position
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="sl"></param>
        /// <param name="tp"></param>
        public Request(long ticket, double sl, double tp)
        {
            mtOp = MTOperation.ModifyOrder;
            this.ticket = ticket;
            this.sl = sl;
            this.tp = tp;
        }

        /// <summary>
        /// Modify the entry price of an existing order
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="entry"></param>
        public Request(long ticket, double entry)
        {
            mtOp = MTOperation.ModifyOrder;
            this.entry = entry;
        }

        #endregion

        #region Cancel Order Request

        /// <summary>
        /// Cancel an existing order
        /// </summary>
        /// <param name="ticket"></param>
        public Request(long ticket)
        {
            mtOp = MTOperation.CancelOrder;
            this.ticket = ticket;
        }

        #endregion

        #region Get Symbol Info Request

        /// <summary>
        /// Grab info on the specified symbol
        /// </summary>
        /// <param name="symbol"></param>
        public Request(string symbol)
        {
            mtOp = MTOperation.SymbolInfo;
            this.symbol = symbol;
        }

        #endregion

        #region Get Account Info Request

        /// <summary>
        /// Grab info on the current account
        /// </summary>
        public Request()
        {
            mtOp = MTOperation.AccountInfo;
        }

        #endregion

        #region Get Symbol History Request
        /// <summary>
        /// Grab candles between 2 dates
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="timeframe"></param>
        public Request(string symbol, DateTime start, DateTime end, TIMEFRAME timeframe)
        {
            mtOp = MTOperation.HistoryInfo;
            this.symbol = symbol;
            this.start = start;
            this.end = end;
            this.timeframe = timeframe;
        }

        /// <summary>
        /// Grab n candles from start date
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="timeframe"></param>
        public Request(string symbol, DateTime start, int count, TIMEFRAME timeframe)
        {
            mtOp = MTOperation.HistoryInfo;
            this.symbol = symbol;
            this.start = start;
            this.count = count;
            this.timeframe = timeframe;
        }

        /// <summary>
        /// Grab n candles coming up to end date
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="count"></param>
        /// <param name="end"></param>
        /// <param name="timeframe"></param>
        public Request(string symbol, int count, DateTime end, TIMEFRAME timeframe)
        {
            mtOp = MTOperation.HistoryInfo;
            this.symbol = symbol;
            this.count = count;
            this.end = end;
            this.timeframe = timeframe;
        }

        #endregion

        #region Subscribe Symbol Request

        /// <summary>
        /// Listen to symbol ticks and orderbook
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="orderbook"></param>
        public Request(string symbol, bool orderbook)
        {
            mtOp = MTOperation.Subscribe;
            this.symbol = symbol;
            this.orderbook = orderbook;
        }

        #endregion

        public string Generate()
        {
            string request = mtOp.ToString() + "|";
            switch (mtOp)
            {
                case MTOperation.NewOrder:
                    {
                        if (type == MTOrderType.Specified && specified != null)
                        {
                            request += symbol + "|" + volume.ToString() + "|" + sl.ToString() + "|" + tp.ToString() + "|" + entry.ToString() + "|" + type.ToString() + "|" + specified.ToString();
                            return request;
                        }
                        request += symbol + "|" + volume.ToString() + "|" + sl.ToString() + "|" + tp.ToString() + "|" + entry.ToString() + "|" + type.ToString();
                        return request;
                    }
                    break;
                case MTOperation.ModifyOrder:
                    {
                        if (sl != 0.0 || tp != 0.0)
                        {
                            request += ticket.ToString() + "|" + sl.ToString() + "|" + tp.ToString();
                            return request;
                        }
                        request += ticket.ToString() + "|" + entry.ToString();
                        return request;
                    }
                    break;
                case MTOperation.CancelOrder:
                    {
                        request += ticket.ToString();
                        return request;
                    }
                    break;
                case MTOperation.SymbolInfo:
                    {
                        request += symbol;
                        return request;
                    }
                    break;
                case MTOperation.AccountInfo:
                    {
                        return request.Replace("|", "");
                    }
                    break;
                case MTOperation.HistoryInfo:
                    {
                        if (start != null)
                        {
                            if (end != null)
                            {
                                request += symbol + "|" + start.ToString() + "|" + end.ToString() + "|" + timeframe.ToString() + "|DATE2DATE";
                                return request;
                            }
                            request += symbol + "|" + start.ToString() + "|" + count.ToString() + "|" + timeframe.ToString() + "|DATE2COUNT";
                            return request;
                        }
                        request += symbol + "|" + count.ToString() + "|" + end.ToString() + "|" + timeframe.ToString() + "|COUNT2DATE";
                        return request;
                    }
                    break;
                case MTOperation.Subscribe:
                    {
                        request += symbol + "|" + orderbook.ToString();
                        return request;
                    }
                    break;
                default:
                    {
                        throw new Exception("Invalid MT Operation requested!");
                    }
                    break;
            }
            return "";
        }
    }

    public class Response
    {
        public bool valid { get; private set; }
        public Response(string rawResponse)
        {
            this.valid = true;

        }
    }

    public enum MTOperation : int
    {
        NewOrder,
        ModifyOrder,
        CancelOrder,
        SymbolInfo,
        AccountInfo,
        HistoryInfo,
        Subscribe
    }

    public enum MTOrderType: int
    {
        GTC,
        FillOrKill,
        Specified
    }

}
