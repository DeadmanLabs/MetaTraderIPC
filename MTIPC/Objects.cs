using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MTIPC.MQL;

namespace MTIPC.Objects
{
    public class Trade
    {
        long ticket;
        DateTime time;
        long timeMSC;
        DateTime timeUpdate;
        long timeUpdateMSC;
        ENUM_POSITION_TYPE type;
        long magic;
        long identifier;
        ENUM_POSITION_REASON reason;
        double volume;
        double priceOpen;
        double sl;
        double tp;
        double priceCurrent;
        double swap;
        double profit;
        string symbol;
        string comment;
        string externalID;
    }

    public class Symbol
    {
        ENUM_SYMBOL_SECTOR sector;
        ENUM_SYMBOL_INDUSTRY industry;
        bool custom;
        Color backgroundColor;
        ENUM_SYMBOL_CHART_MODE chartMode;
        bool exist;
        bool select;
        bool visible;
        long sessionDeals;
        long sessionBuyOrders;
        long sessionSellOrders;
        long volume;
        long volumeHigh;
        long volumeLow;
        DateTime time;
        long timeMSC;
        int digits;
        bool spreadFloat;
        int spread;
        int ticksBookDepth;
        ENUM_SYMBOL_CALC_MODE tradeCalcMode;
        ENUM_SYMBOL_TRADE_MODE tradeMode;
        DateTime startTime;
        DateTime expirationTime;
        int tradeStopsLevel;
        int tradeFreezeLevel;
        ENUM_SYMBOL_TRADE_EXECUTION tradeExeMode;
        ENUM_SYMBOL_SWAP_MODE swapMode;
        ENUM_DAY_OF_WEEK swapRollover3Days;
        bool marginHedgedUseLeg;
        int expirationMode;
        int fillingMode;
        int orderMode;
        ENUM_SYMBOL_ORDER_GTC_MODE orderGTCMode;
        ENUM_SYMBOL_OPTION_MODE optionMode;
        ENUM_SYMBOL_OPTION_RIGHT optionRight;

        double bid;
        double bidHigh;
        double bidLow;
        double ask;
        double askHigh;
        double askLow;
        double last;
        double lastHigh;
        double lastLow;
        double volumeReal;
        double volumeHighReal;
        double volumeLowReal;
        double optionStrike;
        double point;
        double tradeTickValue;
        double tradeTickValueProfit;
        double tradeTickValueLoss;
        double tradeTickSize;
        double tradeContractSize;
        double tradeAccruedInterest;
        double tradeFaceValue;
        double tradeLiquidityRate;
        double volumeMin;
        double volumeMax;
        double volumeStep;
        double volumeLimit;
        double swapLong;
        double swapShort;
        double marginInitial;
        double marginMaintenance;
        double sessionVolume;
        double sessionTurnover;
        double sessionInterest;
        double sessionBuyOrdersVolume;
        double sessionSellOrdersVolume;
        double sessionOpen;
        double sessionClose;
        double sessionAW;
        double sessionPriceSettlement;
        double sessionPriceLimitMin;
        double sessionPriceLimitMax;
        double marginHedged;
        double priceChange;
        double priceVolatility;
        double priceTheoretical;
        double priceDelta;
        double priceTheta;
        double priceGamma;
        double priceVega;
        double priceRho;
        double priceOmega;
        double priceSensitivity;

        string basis;
        string category;
        string country;
        string sectorName;
        string industryName;
        string currencyBase;
        string currencyProfit;
        string currencyMargin;
        string bank;
        string description;
        string exchange;
        string formula;
        string ISIN;
        string page;
        string path;
    }

    public class Account
    {

    }

    public class Candle
    {

    }

    public class Order
    {

    }

    public struct Tick
    {
        DateTime time;
        double bid;
        double ask;
        double last;
        ulong volume;
        long time_msc;
        TICK_FLAG flags;
        double volume_real;
    }

    public struct Book
    {
        ENUM_BOOK_TYPE type;
        double price;
        long volume;
        double volume_real;
    }
}
